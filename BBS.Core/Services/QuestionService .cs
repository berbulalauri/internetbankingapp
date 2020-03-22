using BBS.DAL.Repositories;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
private ILogger _logger;
        public QuestionService(IQuestionRepository questionRepository, ILogger logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }
        public async Task<List<Question>> GetList()
        {
            return (await _questionRepository.GetAllAsync());
        }
        public async Task Add(Question question)
        {
            _logger.Info($"Add Question: {question}");
            await _questionRepository.InsertAsync(question);
            await _questionRepository.SaveAsync();
        }

        public async Task UpdateQuestion(Question question)
        {
            _logger.Info($"Update Question: {question}");
            await _questionRepository.UpdateQuestionAsync(question);

        }
        public async Task DeleteQuestionService(int id)
        {
            _logger.Info($"Delete Question: {id.ToString()}");
            await _questionRepository.DeleteAsync(id);
            await _questionRepository.SaveAsync();

        }
        public async Task<Question> GetQuestion(int? Id)
        {
            var question = await _questionRepository.GetQuestionContext(Id);
            return question;
        }
           
}
}
