using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Input
{
	public class JSONInputParser : IInputParser
	{
		private readonly JsonSerializerSettings Settings;

		public JSONInputParser() : this(new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error }) {}
		public JSONInputParser(JsonSerializerSettings jsonSettings)
		{
			if (jsonSettings == null) {
				throw new ArgumentNullException("jsonSettings");
			}

			this.Settings = jsonSettings;
		}

		public InputParameters Parse(string data)
		{
			if (string.IsNullOrWhiteSpace(data)) {
				throw new ArgumentException("Data is invalid");
			}

			return JsonConvert.DeserializeObject<InputParameters>(data, this.Settings);
		}
	}
}
