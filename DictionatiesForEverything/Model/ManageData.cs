using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionatiesForEverything.Model
{
    public static class ManageData
    {
        // Получить список всех глоссариев

        public static ObservableCollection<Glossary> GetGlossaries()
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                applicationContext.Glossaries.Load();
                return new ObservableCollection<Glossary>(applicationContext.Glossaries.Local.ToObservableCollection());
            }
        }

        // Получить список всех элементов глоссариев
        public static ObservableCollection<GlossaryItem> GetGlossaryItems()
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                applicationContext.GlossaryItems.Load();
                return new ObservableCollection<GlossaryItem>(applicationContext.GlossaryItems.Local.ToObservableCollection());
            }
        }

        // Добавить новый глоссарий

        public static void AddGlossary(string title)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                applicationContext.Add(new Glossary
                {
                    Name = title
                });
                applicationContext.SaveChanges();
            }
        }

        // Добавить новый элемент глоссария

        public static void AddGlossaryItem(string term, string description, int glossaryId)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                applicationContext.Add(new GlossaryItem
                {
                    Name = term,
                    Description = description,
                    GlossaryId = glossaryId
                });
                applicationContext.SaveChanges();
            }
        }

        // Обновить элемент глоссария

        public static void UpdateGlossaryItem(int id, string name, string description)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var item = applicationContext.GlossaryItems.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    item.Name = name;
                    item.Description = description;
                    applicationContext.SaveChanges();
                }
            }
        }

        // Удалить элемент глоссария

        public static void DeleteGlossaryItem(int id)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var item = applicationContext.GlossaryItems.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    applicationContext.GlossaryItems.Remove(item);
                    applicationContext.SaveChanges();
                }
            }
        }
    }
}
