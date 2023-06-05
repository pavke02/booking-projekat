using System;
using System.ComponentModel;
using SIMS_Booking.Enums;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.Commands.OwnerCommands
{
    public class ReportCommentCommand : CommandBase
    {
        private readonly CommentService _commentService;
        private readonly ForumViewModel _viewModel;

        public ReportCommentCommand(CommentService commentService, ForumViewModel viewModel)
        {
            _commentService = commentService;
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.SelectedComment.NumberOfReports += 1;
            _commentService.Update(_commentService.GetById(_viewModel.SelectedComment.GetId()));
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedComment != null && 
                   _viewModel.SelectedComment.OwnerStatus == CommentOwnerStatus.HasNotBeen &&
                   _viewModel.SelectedComment.NumberOfReports == 0 &&
                   base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ForumViewModel.SelectedComment))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
