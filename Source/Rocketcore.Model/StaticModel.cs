#region content (/sitecore/content)
namespace LM.Model.Static {
	using System;
	/// <summary><para>Sitecore Path: /sitecore/content</para><para>Template: Main section</para></summary>
	public static partial class Content {
		public static Guid Id = new Guid("{0DE95AE4-41AB-4D01-9EB0-67441B7C2450}");
		public static string Name { get { return "content"; } }
		public static string Path { get { return "/sitecore/content"; } }
		public static string TemplateName { get { return "Main section"; } }
	}
}
#endregion
#region system (/sitecore/system)
namespace LM.Model.Static {
	using System;
	/// <summary><para>Sitecore Path: /sitecore/system</para><para>Template: Main section</para></summary>
	public static partial class ScSystem {
		public static Guid Id = new Guid("{13D6D6C6-C50B-4BBD-B331-2B04F1A58F21}");
		public static string Name { get { return "system"; } }
		public static string Path { get { return "/sitecore/system"; } }
		public static string TemplateName { get { return "Main section"; } }
	}
}
#endregion
#region Presentation Settings (/sitecore/system/Presentation Settings)
namespace LM.Model.Static {
	using System;
	public static partial class ScSystem {
		/// <summary><para>Sitecore Path: /sitecore/system/Presentation Settings</para><para>Template: Presentation Settings</para></summary>
		public static partial class PresentationSettings {
			public static Guid Id = new Guid("{2369203D-C4DD-4862-B0B6-2DBFEFD391AF}");
			public static string Name { get { return "Presentation Settings"; } }
			public static string Path { get { return "/sitecore/system/Presentation Settings"; } }
			public static string TemplateName { get { return "Presentation Settings"; } }
		}
	}
}
#endregion
#region Filter Options (/sitecore/system/Presentation Settings/Filter Options)
namespace LM.Model.Static {
	using System;
	public static partial class ScSystem {
		public static partial class PresentationSettings {
			/// <summary><para>Sitecore Path: /sitecore/system/Presentation Settings/Filter Options</para><para>Template: Rocketcore Folder</para></summary>
			public static partial class FilterOptions {
				public static Guid Id = new Guid("{26C8CC7B-22DB-4E49-B360-F7EF98078BCC}");
				public static string Name { get { return "Filter Options"; } }
				public static string Path { get { return "/sitecore/system/Presentation Settings/Filter Options"; } }
				public static string TemplateName { get { return "Rocketcore Folder"; } }
			}
		}
	}
}
#endregion
