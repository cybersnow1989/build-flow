using Android.Graphics.Pdf;
using BuildFlow.Model;
using BuildFlow.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Drawing;
using Xamarin.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using static Android.Resource;
using static Android.Views.WindowInsetsAnimation;
using System.Data;
using System.IO;
using PdfDocument = Syncfusion.Pdf.PdfDocument;
using Android.Locations;

namespace BuildFlow.ViewModel
{
    public class InvoiceDetailsViewModel : BaseValidationViewModel<Invoice>
    {
        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));
        private Command _deleteCommand;
        public Command DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(async () => await Delete()));
        public Command AddItemCommand => new Command(async () => await AddItem());
        public Command GeneratePdfCommand => new Command(async () => await GeneratePdf(SelectedInvoice, LineItems));

        public Command SelectLineItemCommand => new Command<LineItem>(async lineItem => await SelectLineItem(lineItem));
        public Command UpdateLineItemCommand => new Command(async () => await UpdateLineItem(SelectedLineItem));
        public Command DeleteLineItemCommand => new Command(async () => await DeleteLineItem(SelectedLineItem));

        #region Properties
        public Invoice SelectedInvoice { get; set; }

        private decimal _invoiceTotal;
        public decimal InvoiceTotal
        {
            get => _invoiceTotal;
            set
            {
                _invoiceTotal = value;
                OnPropertyChanged();
            }
        }

        private LineItem _selectedLineItem;
        public LineItem SelectedLineItem
        {
            get => _selectedLineItem;
            set
            {
                _selectedLineItem = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LineItem> _lineItems;
        public ObservableCollection<LineItem> LineItems
        {
            get => _lineItems;
            set
            {
                _lineItems = value;
                OnPropertyChanged();
            }
        }

        private string _itemDescription;
        public string ItemDescription
        {
            get => _itemDescription;
            set
            {
                _itemDescription = value;
                OnPropertyChanged();
                AddItemCommand.ChangeCanExecute();
            }
        }

        private string _itemPrice;
        public string ItemPrice
        {
            get => _itemPrice;
            set
            {
                _itemPrice = value;
                OnPropertyChanged();
                AddItemCommand.ChangeCanExecute();
            }
        }

        private bool _updateButtonEnabled;
        public bool UpdateButtonEnabled
        {
            get => _updateButtonEnabled;
            set
            {
                _updateButtonEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _deleteButtonEnabled;
        public bool DeleteButtonEnabled
        {
            get => _deleteButtonEnabled;
            set
            {
                _deleteButtonEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion


        public InvoiceDetailsViewModel(INavService navService) : base(navService)
        {
            SelectedInvoice = new Invoice();
        }

        public override void Init(Invoice selectedInvoice)
        {
            SelectedInvoice = selectedInvoice;

            LineItems = new ObservableCollection<LineItem>(LineItem.GetLineItemsByInvoiceID(selectedInvoice.ID));
            InvoiceTotal = LineItems.Sum(x => x.ItemPrice);
        }

        async Task AddItem()
        {
            string errorMessage = string.Empty;
            bool isError = false;
            decimal itemPriceDecimal;

            Decimal.TryParse(ItemPrice, out itemPriceDecimal);

            if (string.IsNullOrWhiteSpace(ItemDescription))
            {
                errorMessage += "Item Description is required." + Environment.NewLine;
                isError = true;
            }

            if (itemPriceDecimal == 0m)
            {
                errorMessage += "Item price cannot be zero.";
                isError = true;
            }

            if (!isError)
            {
                var newLineItem = new LineItem
                {
                    ItemDescription = ItemDescription,
                    ItemPrice = itemPriceDecimal
                };
                LineItems.Add(newLineItem);
                InvoiceTotal = LineItems.Sum(x => x.ItemPrice);
                ItemPrice = string.Empty;
                ItemDescription = string.Empty; 
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", errorMessage, "Ok");
            }
        }

        async Task SelectLineItem(LineItem lineItem)
        {
            SelectedLineItem = lineItem;
            ItemDescription = lineItem.ItemDescription;
            ItemPrice = lineItem.ItemPrice.ToString();
            UpdateButtonEnabled = true;
            DeleteButtonEnabled = true;
        }

        async Task UpdateLineItem(LineItem lineItem)
        {
            string errorMessage = string.Empty;
            bool isError = false;
            decimal itemPriceDecimal;

            Decimal.TryParse(ItemPrice, out itemPriceDecimal);

            if (string.IsNullOrWhiteSpace(ItemDescription))
            {
                errorMessage += "Item Description is required." + Environment.NewLine;
                isError = true;
            }

            if (itemPriceDecimal == 0m)
            {
                errorMessage += "Item price cannot be zero.";
                isError = true;
            }

            if (!isError)
            {
                int newIndex = LineItems.IndexOf(lineItem);
                LineItems.Remove(lineItem);

                var updateLineItem = new LineItem
                {
                    ItemDescription = ItemDescription,
                    ItemPrice = itemPriceDecimal
                };
                LineItems.Add(updateLineItem);
                int oldIndex = LineItems.IndexOf(updateLineItem);
                LineItems.Move(oldIndex, newIndex);
                InvoiceTotal = LineItems.Sum(x => x.ItemPrice);
                ItemPrice = string.Empty;
                ItemDescription = string.Empty;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", errorMessage, "Ok");
            }
        }

        async Task DeleteLineItem(LineItem lineItem)
        {
            decimal itemPriceDecimal;

            Decimal.TryParse(ItemPrice, out itemPriceDecimal);

            var updateLineItem = new LineItem
            {
                ItemDescription = ItemDescription,
                ItemPrice = itemPriceDecimal
            };
            LineItems.Remove(lineItem);
            UpdateButtonEnabled = false;
            DeleteButtonEnabled = false;
        }

        async Task Save()
        {
            SelectedInvoice.InvoiceTotal = InvoiceTotal;

            if (SelectedInvoice.CreatedOn.Year == 1)
            {
                SelectedInvoice.CreatedOn = DateTime.Now;
            }

            var lineItemsToDelete = LineItem.GetLineItemsByInvoiceID(SelectedInvoice.ID);

            bool successLineItemsDelete = LineItem.DeleteLineItems(lineItemsToDelete);

            bool insertLineItemsSuccess = false;

            foreach (LineItem lineItem in LineItems)
            {
                lineItem.InvoiceID = SelectedInvoice.ID;
            }

            var insertedLineItems = LineItem.InsertLineItems(LineItems.ToList());

            foreach (LineItem insertedLineItem in insertedLineItems)
            {
                if (insertedLineItem.ID != 0)
                {
                    insertLineItemsSuccess = true;
                }
                else
                {
                    insertLineItemsSuccess = false;
                    break;
                }
            }

            if (Invoice.Update(SelectedInvoice) && insertLineItemsSuccess && successLineItemsDelete)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Invoice successfully saved.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Invoice not saved.", "Ok");
            }

            await NavService.GoBack();
        }

        async Task Delete()
        {
            if (Invoice.Delete(SelectedInvoice))
            {
                await App.Current.MainPage.DisplayAlert("Success", "Invoice successfully deleted.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Invoice not deleted.", "Ok");
            }

            await NavService.GoBack();
        }

        async Task GeneratePdf(Invoice currentInvoice, ObservableCollection<LineItem> currentLineItems)
        {
            var job = Job.GetJobs().FirstOrDefault(x => x.ID == currentInvoice.JobID);
            var customer = Customer.GetCustomers().FirstOrDefault(x => x.ID == job.InvoiceID);
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            document.PageSettings.Orientation = PdfPageOrientation.Landscape;
            document.PageSettings.Margins.All = 50;

            //Add a page to the document.
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;

            //Create a text element with the text and font.
            PdfTextElement element = new PdfTextElement($"{CurrentUser.CompanyName}\n{CurrentUser.Address}\n{CurrentUser.City}, {CurrentUser.ZipCode}");
            element.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            PdfLayoutResult result = element.Draw(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 200));

            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            graphics.DrawRectangle(new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(0, result.Bounds.Bottom + 40, graphics.ClientSize.Width, 30));

            //Create a text element with the text and font.
            element = new PdfTextElement("INVOICE", subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 48));
            string currentDate = "DATE " + DateTime.Now.ToString("MM/dd/yyyy");
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

            //Create a text element and draw it to PDF page.
            element = new PdfTextElement("BILL TO ", new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));
            graphics.DrawLine(new PdfPen(new PdfColor(126, 151, 173), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3));

            //Create a text element and draw it to PDF page.
            element = new PdfTextElement(string.Format($"{customer.LastName}, {customer.FirstName}\n{customer.Address}\n{customer.City}, {customer.ZipCode}"), new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, graphics.ClientSize.Width / 2, 100));

            //Create a PDF grid with product details.
            PdfGrid grid = new PdfGrid();
            grid.DataSource = currentLineItems.Select(x => new { x.ID, x.ItemDescription, x.ItemPrice }).ToList();

            //Initialize PdfGridCellStyle and set border color.
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));


            //Initialize PdfGridCellStyle and set header style.
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

            PdfGridRow header = grid.Headers[0];
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 1)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            }
            header.ApplyStyle(headerStyle);

            foreach (PdfGridRow row in grid.Rows)
            {
                row.ApplyStyle(cellStyle);
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    //Create and customize the string formats
                    PdfGridCell cell = row.Cells[i];
                    if (i == 1)
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else if (i == 0)
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);

                    if (i > 2)
                    {
                        float val = float.MinValue;
                        float.TryParse(cell.Value.ToString(), out val);
                        cell.Value = '$' + val.ToString("0.00");
                    }
                }
            }

            grid.Columns[0].Width = 100;
            grid.Columns[1].Width = 200;

            //Set properties to paginate the grid.
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;

            //Draw grid to the page of PDF document.
            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);
            float pos = 0.0f;
            for (int i = 0; i < grid.Columns.Count - 1; i++)
                pos += grid.Columns[i].Width;

            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f);


            gridResult.Page.Graphics.DrawString("Total Due", font, new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(new PointF(pos, gridResult.Bounds.Bottom + 20), new SizeF(grid.Columns[2].Width - pos, 20)), new PdfStringFormat(PdfTextAlignment.Right));
            gridResult.Page.Graphics.DrawString("Thank you for your business!", new PdfStandardFont(PdfFontFamily.TimesRoman, 12), new PdfSolidBrush(new PdfColor(89, 89, 93)), new PointF(pos - 55, gridResult.Bounds.Bottom + 60));
            pos += grid.Columns[2].Width;
            gridResult.Page.Graphics.DrawString('$' + string.Format("{0:N2}", currentInvoice.InvoiceTotal.ToString()), font, new PdfSolidBrush(new PdfColor(131, 130, 136)), new RectangleF(new PointF(pos, gridResult.Bounds.Bottom + 20), new SizeF(grid.Columns[2].Width - pos, 20)), new PdfStringFormat(PdfTextAlignment.Right));

            //Save the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            //Close the document.
            document.Close(true);

            //Save the stream as a file in the device and invoke it for viewing
            await Xamarin.Forms.DependencyService.Get<ISavePDF>().SaveAndView($"Invoice-{currentInvoice.ID.ToString()}.pdf", "application/pdf", stream);
        }

        bool CanSave() => !Helpers.Validators.CheckIfZeroOrNegative(InvoiceTotal) && !HasErrors;
    }
}
