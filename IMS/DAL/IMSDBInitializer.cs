using IMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace IMS.DAL
{
    public class IMSDBInitializer : CreateDatabaseIfNotExists<IMSDBContext>
    {
        protected override void Seed(IMSDBContext context)
        {
            base.Seed(context);

            List<AccountLevel> accountLevels = new List<AccountLevel>()
            {
                new AccountLevel
                {
                    Level="admin",
                    LevelName="系統管理員",
                    Status="Y",
                    CreDate=DateTime.Now,
                }
            };
            accountLevels.ForEach(s => context.AccountLevel.Add(s));
            context.SaveChanges();


            List<Account> accounts = new List<Account>()
            {
                new Account
                {
                    AccountNo="matt",
                    AccountName="matt",
                    Password="123456",
                    Email="jdiin37@gmail.com",
                    CreDate=DateTime.Now,
                    Status="Y",
                    Level="admin"
                }
            };
            accounts.ForEach(s => context.Account.Add(s));
            context.SaveChanges();

            List<PigFarm> pigFarms = new List<PigFarm>()
            {
                new PigFarm
                {
                    Name="測試養豬場",
                    CreDate=DateTime.Now,
                    Status="Y"
                }
            };
            pigFarms.ForEach(s => context.PigFarm.Add(s));
            context.SaveChanges();

            List<SeqNo> seqNos = new List<SeqNo>()
            {
                new SeqNo
                {
                    Name="TraceNo",
                    CurrentValue = 0,
                    IncrementValue = 1,
                    ModDate=DateTime.Now,
                },
                new SeqNo
                {
                    Name="TraceNoSeqNo",
                    CurrentValue = 0,
                    IncrementValue = 1,
                    ModDate=DateTime.Now,
                },
            };
            seqNos.ForEach(s => context.SeqNo.Add(s));
            context.SaveChanges();

        }

        private byte[] getFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }

            return fileBytes;
        }
    }
}