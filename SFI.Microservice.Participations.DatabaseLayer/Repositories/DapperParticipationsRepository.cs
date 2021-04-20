using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using SFI.Microservice.Common.DatabaseLayer;
using SFI.Microservice.Common.DatabaseModel;

namespace SFI.Microservice.Participants.DatabaseLayer.Repositories
{
    public class DapperParticipationsRepository : IRepository<Participant>
    {
        private string _connectionString;
        public DapperParticipationsRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:ParticipationsDbContext"];
        }

        public Participant Create(Participant input)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql =
                    @"select * from dbo.Participants p
inner join dbo.Users u on p.UserId = u.Id
inner join dbo.Events e on p.EventId = e.Id";

                db.Execute($"INSERT INTO dbo.Participants (UserId, EventId, Confirmed, IsDeleted) VALUES ('{input.User.Id}', '{input.Event.Id}', 'False', 'False')");
                var data = db.Query<Participant, User, EventItem, Participant>(sql, (p, u, e) =>
                {
                    p.Event = e;
                    p.User = u;
                    return p;
                });

                return data.ToList().FirstOrDefault();
            }
        }

        public Participant Read(long id)
        {
            throw new NotImplementedException();
        }

        public List<Participant> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                var sql =
                    @"select * from dbo.Participants p
inner join dbo.Users u on p.UserId = u.Id
inner join dbo.Events e on p.EventId = e.Id";

                var data = db.Query<Participant, User,EventItem, Participant>(sql, (p,u, e) =>
                {
                    p.Event = e;
                    p.User = u;

                    return p;
                });

                return data.ToList();
            }
        }

        public Participant Update(Participant input)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql =
                    @"select * from dbo.Participants p
inner join dbo.Users u on p.UserId = u.Id
inner join dbo.Events e on p.EventId = e.Id";

                db.Execute($"UPDATE dbo.Participants SET UserId = '{input.User.Id}', EventId='{input.Event.Id}', Confirmed = 'True' WHERE Id = '{input.Id}'");
                var data = db.Query<Participant, User, EventItem, Participant>(sql, (p, u, e) =>
                {
                    p.Event = e;
                    p.User = u;
                    return p;
                });

                return data.ToList().FirstOrDefault();
            }
        }
    }
}
