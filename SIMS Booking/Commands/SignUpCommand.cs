using System.ComponentModel;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel.Startup;

namespace SIMS_Booking.Commands
{
    public class SignUpCommand : CommandBase
    {
        private readonly SignUpViewModel _viewModel;
        private readonly UserService _userService;

        public SignUpCommand(SignUpViewModel signUpViewModel, UserService userService)
        {
            _viewModel = signUpViewModel;
            _userService = userService;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.ErrorText = _viewModel.ConfirmPassword != _viewModel.Password;
            if (_viewModel.ErrorText) return;

            User user = new User(_viewModel.Username, _viewModel.Password, _viewModel.Role, false, uint.Parse(_viewModel.Age));
            _userService.Save(user);
            MessageBox.Show("User registered!");
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.Username) && !(_viewModel.Username.Length < 4) &&
                   !string.IsNullOrEmpty(_viewModel.Password) && _userService.GetByUsername(_viewModel.Username) == null &&
                   !string.IsNullOrEmpty(_viewModel.Age) && uint.TryParse(_viewModel.Age, out _) && !(uint.Parse(_viewModel.Age) < 1);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SignUpViewModel.Username) || e.PropertyName == nameof(SignUpViewModel.Password) ||
                e.PropertyName == nameof(SignUpViewModel.Age))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
