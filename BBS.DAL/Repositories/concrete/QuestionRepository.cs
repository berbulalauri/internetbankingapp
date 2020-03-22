using BBS.DAL.Database;
using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(BankDbContext ctx) : base(ctx)
        {
        }
        public async Task UpdateQuestionAsync(Question question)
        {
            try
            {
                _context.Update(question);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteQuestionAsync(int id)
        {
            try
            {
                var QuestionRemove = _context.Questions.Where(x=>x.Id==id).FirstOrDefault();
                QuestionRemove.IsDeleted = true;
                    await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _context.SaveChangesAsync();
            }
        }
      


        public async Task<Question> GetQuestionContext(int? id)

        {
            return await _context.Questions.FindAsync(id);
        }
    }
}


