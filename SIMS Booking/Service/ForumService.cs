using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class ForumService
    {
        private readonly ICRUDRepository<Forum> _repository;

        public ForumService(ICRUDRepository<Forum> repository)
        {
            _repository = repository;
        }

        #region Crud
        public List<Forum> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(Forum forum)
        {
            _repository.Update(forum);
        }

        public Forum GetById(int id)
        {
            return _repository.GetById(id);
        }
        #endregion

        public void LoadForumCreatorAndCommentsInForum(UserService userService, CommentService commentService)
        {
            foreach (Forum forum in _repository.GetAll())
            {
                forum.CreatedBy = userService.GetById(forum.CreatedById);
                forum.Comments = commentService.GetAll().Where(x => x.ForumId == forum.GetId()).ToList();
                forum.OwnersComments = forum.Comments.Count(x => x.OwnerStatus == CommentOwnerStatus.Owner);
                forum.GuestsComments = forum.Comments.Count(x => x.OwnerStatus == CommentOwnerStatus.HasBeen);
            }
        }

        public List<Forum> ShouldNotifyOwner(int ownerId)
        {
            return GetAll().Where(x => x.OwnersToNotify.ContainsKey(ownerId) && !x.OwnersToNotify[ownerId]).ToList();
        }

    }
}
