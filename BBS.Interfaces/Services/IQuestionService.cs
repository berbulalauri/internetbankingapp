using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface IQuestionService 
    {
        public Task<List<Question>> GetList();
        public Task<Question> GetQuestion(int? Id);
        public Task Add(Question question);
        public Task UpdateQuestion(Question question);
        public  Task DeleteQuestionService(int Id);
        
    }
}
