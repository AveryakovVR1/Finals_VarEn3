using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Finals_VarEn3.Models;

namespace Finals_VarEn3.ViewModels
{


    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private readonly DataService _dataService;
        private Employee _selectedEmployee;

        public ObservableCollection<Employee> Employees { get; }
        public ICommand RegisterCommand { get; }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        public RegistrationViewModel()
        {
            _dataService = new DataService();
            Employees = new ObservableCollection<Employee>(_dataService.GetEmployees());
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        private bool CanRegister(object parameter) => SelectedEmployee != null;

        private void Register(object parameter)
        {
            var newUser = new User
            {
                Login = "NewUser",
                Password = "password",
                EmployeeId = SelectedEmployee.Id,
                RoleId = 2 
            };
            _dataService.AddUser(newUser);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
