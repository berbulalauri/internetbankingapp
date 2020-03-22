using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        public Task UpdateQuestionAsync(Question question);
        public Task<Question> GetQuestionContext(int? Id);
        public Task DeleteQuestionAsync(int id);
    }
}
