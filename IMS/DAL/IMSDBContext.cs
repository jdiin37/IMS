using IMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IMS.DAL
{
    public class IMSDBContext : DbContext
    {
        public IMSDBContext()
            : base("name=IMS")
        {
            Database.SetInitializer(new IMSDBInitializer());
        }

        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。

        //public virtual DbSet<PigResume> PigResume { get; set; }
        public  DbSet<SheetDisinfection> SheetDisinfection { get; set; }
        public  DbSet<Category> Category { get; set; }
        public  DbSet<CategorySub> CategorySub { get; set; }
        public  DbSet<AccountLevel> AccountLevel { get; set; }
        public  DbSet<Account> Account { get; set; }

        public  DbSet<ActionLog> ActionLog { get; set; }
        public  DbSet<RequestLog> RequestLog { get; set; }

        public  DbSet<AccountSession> AccountSession { get; set; }

        public  DbSet<PigFarm> PigFarm { get; set; }
        public  DbSet<FarmDataBase> FarmDataBase { get; set; }
        public  DbSet<FarmDataLand> FarmDataLand { get; set; }
        public  DbSet<FarmDataLicense> FarmDataLicense { get; set; }

        public  DbSet<Photo> Photo { get; set; }

        public  DbSet<WorkBasic> WorkBasic { get; set; }
        public  DbSet<TraceMaster> TraceMaster { get; set; }
        public  DbSet<TraceDetail> TraceDetail { get; set; }

        public  DbSet<SeqNo> SeqNo { get; set; }
    }
}