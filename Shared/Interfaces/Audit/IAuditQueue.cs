
namespace Shared.Interfaces.Audit
{
	public interface IAuditQueue
	{
		void QueueAuditLog(AuditLogDTO logDto);
		ValueTask<AuditLogDTO> DequeueAsync(CancellationToken cancellationToken);
	}
}
