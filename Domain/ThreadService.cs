namespace Domain
{
	public class ThreadService : IThreadService
	{
		public Task GetThreads()
		{
			return Task.CompletedTask;
		}
	}
}
