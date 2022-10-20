using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Android;
using BuildFlow.Services;

namespace BuildFlow.ViewModel
{
    public class BaseValidationViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        readonly IDictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public BaseValidationViewModel(INavService navService) : base(navService)
        {
        }

        public bool HasErrors => _errors.Any(x => x.Value?.Any() == true) == true;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _errors.SelectMany(x => x.Value);
            }

            if (_errors.ContainsKey(propertyName) && _errors[propertyName].Any())
            {
                return _errors[propertyName];
            }

            return new List<string>();
        }

        protected void Validate(Func<bool> rule, string error, [CallerMemberName] string propertyName = "")
        {
            if(string.IsNullOrWhiteSpace(propertyName)) return;

            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }

            if (rule() == false)
            {
                _errors.Add(propertyName, new List<string> {error});
            }

            OnPropertyChanged(nameof(HasErrors));

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

    public class BaseValidationViewModel<TParameter> : BaseValidationViewModel
    {
        protected BaseValidationViewModel(INavService navService) : base(navService)
        {
        }

        public override void Init()
        {
            Init(default(TParameter));
        }

        public virtual void Init(TParameter parameter)
        {
        }
    }
}
