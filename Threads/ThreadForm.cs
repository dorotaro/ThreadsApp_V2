using Domain;

namespace ThreadApp
{
	public partial class Form1 : Form
	{

		private CancellationTokenSource cts = new CancellationTokenSource();
		private int TableRows = 1;
		private readonly IThreadService _threadService;
		public Form1(IThreadService threadService)
		{
			_threadService = threadService;
			InitializeComponent();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private async Task<int?> AddThreadToTable(int? taskID)
		{
			await _threadService.GetThreads();
			var randomString = GenerateRandomString();
			var date = DateTime.Now.ToString();
			TableRows++;
			ListViewItem item1 = new ListViewItem(TableRows.ToString());
			item1.SubItems.Add(randomString);
			item1.SubItems.Add(date);
			item1.SubItems.Add(taskID.ToString());

			listView1.Invoke((MethodInvoker)(() => listView1.Items.Add(item1)));
			return taskID;
		}

		private static string GenerateRandomString()
		{
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var stringChars = new char[8];
			var random = new Random();

			for (int i = 0; i < stringChars.Length; i++)
			{
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			return new string(stringChars);
		}

		private void Stop_threading_button(object sender, EventArgs e)
		{
			button2.Enabled = false;
			cts.Cancel();
			button1.Enabled = true;
			cts.Dispose();
			cts = new CancellationTokenSource();
		}

		private void Start_threading_button(object sender, EventArgs e)
		{
			button1.Enabled = false;
			button2.Enabled = true;

			for (var i = 0; i < (int)numericUpDown1.Value; i++)
				{
					Task.Run(async () =>
					{
						var taskId = Task.CurrentId;
						while (true)
						{
							taskId = Task.CurrentId ?? taskId;
							await AddThreadToTable(taskId);
							await Task.Delay(2000, cts.Token);
							if (cts.Token.IsCancellationRequested)
								break;
						}
					});
				}
		
		}
	}
}
