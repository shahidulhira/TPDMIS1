using BDCO.Repository;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace BDCO.Domain.Aggregates
{
    public abstract class AggregateRoot
    {
        [NotMapped]
        //private string UserID { get; set; }
        protected readonly DatabaseContext context = new DatabaseContext();
        //protected readonly ContextStack context = new ContextStack();
        private readonly IRepository<EventStore> ar_repository;
        private string userID = "";

        protected AggregateRoot()
        {
            if (HttpContext.Current != null)
                userID = HttpContext.Current.User.Identity.Name;
        }



        protected void LogEventStore(string refID, string aggregateName, string operationType)
        {
            IRepository<EventStore> _repository = new Repository<EventStore>();
            _repository.SetContext(context);
            EventStore eventStore = new EventStore()
            {
                AuditLogID = 0,
                ReffrenceNo = refID,
                AggregateName = aggregateName,
                OperationType = operationType,
                LogDateTime = DateTime.Now.Date,
                UserID = userID
            };
            _repository.Insert(eventStore);
            _repository.SaveChange();
        }
    }



          



    //public abstract class AggregateRoot //:IEventProvider
    //{
    //    private readonly List<IEvent> _changes;

    //    //public Guid Id { get; public set; }
    //    //public int Version { get; public set; }
    //   // public int EventVersion { get; protected set; }

    //    protected AggregateRoot()
    //    {
    //        _changes = new List<IEvent>();
    //    }

    //    public IEnumerable<IEvent> GetUncommittedChanges()
    //    {
    //        return _changes;
    //    }

    //    public void MarkChangesAsCommitted()
    //    {
    //        _changes.Clear();
    //    }

    //    public void LoadsFromHistory(IEnumerable<IEvent> history)
    //    {
    //        foreach (var e in history) ApplyChange(e, false);
    //       // Version = history.Last().Version;
    //       // EventVersion = Version;
    //    }

    //    protected void ApplyChange(IEvent @event)
    //    {
    //        ApplyChange(@event, true);
    //    }

    //    private void ApplyChange(IEvent @event, bool isNew)
    //    {
    //        dynamic d = this;

    //        d.Handle(Converter.ChangeTo(@event,@event.GetType()));
    //        if (isNew)
    //        {
    //            _changes.Add(@event);
    //        }
    //    }      
    //}
}
