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
    }
}
