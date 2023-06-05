using System.Collections.Generic;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class CommentService
    {
        private readonly ICRUDRepository<Comment> _repository;

        public CommentService(ICRUDRepository<Comment> repository)
        {
            _repository = repository;
        }

        #region Crud
        public List<Comment> GetAll()
        {
            return _repository.GetAll();
        }

        public void Save(Comment comment)
        {
            _repository.Save(comment);
        }

        public Comment GetById(int id)
        {
            return _repository.GetById(id);
        }
        #endregion

        public void LoadCommenterInComment(UserService userService)
        {
            foreach (Comment comment in _repository.GetAll())
            {
                comment.Commenter = userService.GetById(comment.CommenterId);
            }
        }
    }
}
