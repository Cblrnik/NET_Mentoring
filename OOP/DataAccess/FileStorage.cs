using Fundametals.Models;
using OOP_Fundamentals.Interfaces;
using OOP_Fundamentals.Models;
using System.Collections.Generic;

namespace OOP_Fundamentals.Dao
{
    public class FileStorage : IStorage
    {
        private readonly BookFileDao _bookDao = BookFileDao.GetInstance();
        private readonly PatentFileDao _patentDao = PatentFileDao.GetInstance();
        private readonly LocalizedBookFileDao _localizedBookDao = LocalizedBookFileDao.GetInstance();

        private static readonly FileStorage _fileStorage;
        private FileStorage()
        {

        }

        public static FileStorage GetInstance()
        {
            return _fileStorage ?? new FileStorage();
        }

        public void DeleteDocument(Document document)
        {
            if (document is Patent patent)
            {
                _patentDao.Delete(patent);
            }
            else if (document is Book book)
            {
                _bookDao.Delete(book);
            }
            else if(document is LocalizedBook localizedBook) 
            {
                _localizedBookDao.Delete(localizedBook);
            }
        }

        public List<Document> GetAllDocuments(DocumentTypes type = DocumentTypes.All)
        {
            switch (type)
            {
                case DocumentTypes.Book:
                    return new List<Document>(_bookDao.GetAll());
                case DocumentTypes.LocalizedBook:
                    return new List<Document>(_localizedBookDao.GetAll());
                case DocumentTypes.Patent:
                    return new List<Document>(_patentDao.GetAll());
                default:
                    var documents = new List<Document>();
                    documents.AddRange(_patentDao.GetAll());
                    documents.AddRange(_bookDao.GetAll());
                    documents.AddRange(_localizedBookDao.GetAll());
                    return documents;
            }
        }

        public void SaveDocument(Document document)
        {
            if (document is Patent patent)
            {
                _patentDao.Create(patent);
            }
            else if (document is Book book)
            {
                _bookDao.Create(book);
            }
            else if (document is LocalizedBook localizedBook)
            {
                _localizedBookDao.Create(localizedBook);
            }
        }

        public void UpdateDocument(Document document)
        {
            if (document is Patent patent)
            {
                _patentDao.Update(patent);
            }
            else if (document is Book book)
            {
                _bookDao.Update(book);
            }
            else if (document is LocalizedBook localizedBook)
            {
                _localizedBookDao.Update(localizedBook);
            }
        }
    }
}
