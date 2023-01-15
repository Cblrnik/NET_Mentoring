using OOP_Fundamentals.Dao;
using OOP_Fundamentals.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Fundamentals.Services
{
    public static class SearchService
    {
        public static List<object> SearchDocumentsByNumber(int number)
        {
            var books = BookDao.GetInstance().GetAll().Where(x => x.Id == number).ToList();
            var localizedBooks = LocalizedBookDao.GetInstance().GetAll().Where(x => x.Id == number).ToList();
            var patents = PatentDao.GetInstance().GetAll().Where(x => x.Id == number).ToList();

            var outList = new List<object>();
            outList.AddRange(books);
            outList.AddRange(localizedBooks);
            outList.AddRange(patents);
            return outList;
        }
    }
}
