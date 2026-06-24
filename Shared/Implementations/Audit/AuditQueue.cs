namespace Shared.Implementations.Audit
{
	public class AuditQueue : IAuditQueue
	{
		private readonly Channel<AuditLogDTO> _queue;
		public AuditQueue()
		{
			// Cấu hình hàng đợi không giới hạn phần tử, tối ưu cho luồng ghi nhanh
			var options = new UnboundedChannelOptions { SingleReader = true, SingleWriter = false };
			_queue = Channel.CreateUnbounded<AuditLogDTO>(options);
		}
		public async ValueTask<AuditLogDTO> DequeueAsync(CancellationToken cancellationToken)
		{
			return await _queue.Reader.ReadAsync(cancellationToken);
		}

		public void QueueAuditLog(AuditLogDTO logDto)
		{
			_queue.Writer.TryWrite(logDto);
		}
	}
}
