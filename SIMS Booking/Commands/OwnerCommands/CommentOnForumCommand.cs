using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel.Owner;
using System;
using System.ComponentModel;

namespace SIMS_Booking.Commands.OwnerCommands
{
    public class CommentOnForumCommand : CommandBase
    {
        private readonly CommentService _commentService;
        private readonly AccommodationService _accommodationService;
        private readonly User _user;
        private readonly ForumViewModel _viewModel;

        public CommentOnForumCommand(ForumViewModel viewModel, CommentService commentService, AccommodationService accommodationService, User user)
        {
            _commentService = commentService;
            _accommodationService = accommodationService;
            _user = user;

            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            Comment comment = new Comment(_user, _viewModel.CommentText, DateTime.Now, CommentOwnerStatus.Owner, _viewModel.SelectedForum.GetId());
            _viewModel.SelectedForum.Comments.Add(comment);
            _commentService.Save(comment);
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.CommentText != null && _accommodationService.OwnerHasAccommodationOnLocation(_viewModel.SelectedForum.Location, _user.GetId()) && 
                   base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ForumViewModel.CommentText))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
