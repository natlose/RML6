using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RML_Paging
{
    public class ObservableObjectBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region INotifyPropertChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Validation housekeeping
        private Dictionary<string, List<string>> validationErrors = new Dictionary<string, List<string>>();

        protected void ValidationErrorsBegin([CallerMemberName] string propertyName = null)
        {
            validationErrors.Remove(propertyName);
            validationErrors.Add(propertyName, new List<string>());
        }

        protected void ValidationErrorsAdd(string error, [CallerMemberName] string propertyName = null)
        {
            validationErrors[propertyName]?.Add(error);
        }

        protected void ValidationErrorsEnd([CallerMemberName] string propertyName = null)
        {
            if (validationErrors[propertyName].Count == 0) validationErrors.Remove(propertyName);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }

        protected void ClearValidationErrors([CallerMemberName] string propertyName = null)
        {
            validationErrors.Remove(propertyName);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion

        #region INotifyDataErrorInfo
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => (validationErrors.Count > 0);        

        public IEnumerable GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                return validationErrors.Values;
            }
            else
            {
                if (validationErrors.ContainsKey(propertyName)) return validationErrors[propertyName];
                else return null;
            }
        }
        #endregion

    }
}
