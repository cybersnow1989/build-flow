using BuildFlow.Model;
using BuildFlow.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class JobViewModel : BaseViewModel
    {
        public Command AddCommand => new Command(async () => await NavService.NavigateTo<JobNewViewModel>());
        public Command ViewCommand => new Command<Job>(async job => await NavService.NavigateTo<JobDetailsViewModel, Job>(job));
        public Command SearchCommand => new Command(async () => await Search());

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public List<Job> Jobs { get; set; }

        private ObservableCollection<Job> _jobList;

        public ObservableCollection<Job> JobList
        {
            get => _jobList;
            set
            {
                _jobList = value;
                OnPropertyChanged();
            }
        }

        public JobViewModel(INavService navService) : base(navService)
        {
            JobList = new ObservableCollection<Job>();
            Jobs = new List<Job>();
        }

        public override void Init()
        {
            LoadJobs();
        }

        void LoadJobs()
        {
            var jobs = Job.GetJobs();
            Jobs.Clear();
            JobList.Clear();

            foreach (Job job in jobs)
            {
                Jobs.Add(job);
                JobList.Add(job);
            }
        }

        async Task Search()
        {
            var results = Jobs.Where(x => x.JobName.ToLower().Contains(SearchText.ToLower())).ToList();
            JobList.Clear();

            foreach (var result in results)
            {
                JobList.Add(result);
            }
        }
    }
}
