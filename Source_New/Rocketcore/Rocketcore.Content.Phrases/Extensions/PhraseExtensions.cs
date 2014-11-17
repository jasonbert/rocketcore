using Fortis.Model.Fields;
using Rocketcore.Model.Templates.UserDefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Rocketcore.Content.Phrases.Extensions
{
	public static class PhraseExtensions
	{
		public static IHtmlString RenderPhrase(this IPhrase phrase, string phraseNotFoundText = null, bool editing = true)
		{
			if (phrase != null)
			{
				ITextFieldWrapper phraseField = null;

				if (phrase is ITextPhrase)
				{
					phraseField = ((ITextPhrase)phrase).Phrase;
				}
				else if (phrase is IHtmlPhrase)
				{
					phraseField = ((IHtmlPhrase)phrase).Phrase;
				}

				if (phraseField != null)
				{
					return phraseField.Render(editing: editing);
				}
			}

			if (phraseNotFoundText == null)
			{
				return new HtmlString("#PhraseItemNotFound#");
			}

			return new HtmlString(string.Format("#{0}#", phraseNotFoundText));
		}
	}
}
