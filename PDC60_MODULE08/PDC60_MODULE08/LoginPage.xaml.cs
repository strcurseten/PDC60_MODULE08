using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

using System.Net.Http;

using System.Collections.ObjectModel;

using static PDC60_MODULE08.SearchPage;

namespace PDC60_MODULE08
{	
	public partial class LoginPage : ContentPage
	{
        private const string url_login = "http://172.20.10.4/pdc60/api-login.php";
		private readonly HttpClient _client;

        public LoginPage ()
		{
			InitializeComponent ();
			_client = new HttpClient();
		}

		public async void OnLogin(object sender, EventArgs e)
		{
			string username = xUsername.Text;
			string password = xPassword.Text;

			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
			{
                await DisplayAlert("Error", "Username and password are required", "OK");
				return;
            }
			try
			{
				var loginUrl = $"{url_login}/?username={username}&password={password}";
				var content = await _client.GetStringAsync(loginUrl);

				var responseObject = JsonConvert.DeserializeObject<ResponseObject>(content);
				if (responseObject.status)
				{
					await DisplayAlert("Success", "Login successful", "OK");

					await Navigation.PushAsync(new MainPage());
				}
				else
				{
                    await DisplayAlert("Error", "Login failed", "OK");
                }
            }
			catch (Exception ex)
			{
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
	}
}

