using System.Collections.ObjectModel;
using System.Windows.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class ForumViewModel : ViewModelBase, IObserver
    {
        private readonly CommentService _commentService;
        private readonly AccommodationService _accommodationService;
        private readonly User _user;

        #region Property
        public ObservableCollection<Comment> Comments { get; set; }
        public Forum SelectedForum { get; set;  }

        private Comment _selectedComment;
        public Comment SelectedComment
        {
            get => _selectedComment;
            set
            {
                if (value != _selectedComment)
                {
                    _selectedComment = value;
                    OnPropertyChanged();
                    FillTextBox();
                }
            }
        }

        private string _selectedCommentText;
        public string SelectedCommentText
        {
            get => _selectedCommentText;
            set
            {
                if (value != _selectedCommentText)
                {
                    _selectedCommentText = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _commentText;
        public string CommentText
        {
            get => _commentText;
            set
            {
                if (value != _commentText)
                {
                    _commentText = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public ICommand NavigateBackCommand { get; }
        public ICommand LeaveCommentCommand { get; }
        public ICommand ReportCommentCommand { get; }

        public ForumViewModel(CommentService commentService, AccommodationService accommodationService, Forum selectedForum, User user, ModalNavigationStore modalNavigationStore)
        {
            _accommodationService = accommodationService;
            SelectedForum = selectedForum;
            _user = user;

            _commentService = commentService;
            _commentService.Subscribe(this);
            Comments = new ObservableCollection<Comment>(SelectedForum.Comments);

            LeaveCommentCommand = new CommentOnForumCommand(this, _commentService, _accommodationService, _user);
            ReportCommentCommand = new ReportCommentCommand(_commentService, this);
            NavigateBackCommand =
                new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

        private void FillTextBox()
        {
            if (SelectedComment != null)
                SelectedCommentText = SelectedComment.Text;
        }

        public void Update()
        {
            Comments.Clear();
            foreach (Comment comment in SelectedForum.Comments)
            {
                Comments.Add(comment);
            }
        }
    }
}
