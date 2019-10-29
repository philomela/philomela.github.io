using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Subtone.ru.Models
{
    public class DBInitializer : DropCreateDatabaseAlways<UsersContext>
    {
        //protected override void Seed(UsersContext db)
        //{
        //    base.Seed(db);
        //}
    }
}