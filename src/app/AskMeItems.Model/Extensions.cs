using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskMeItems.Model
{
	public static class Extensions
	{
        public static QuestionnairePresenter ToPresenter(this Questionnaire questionnaire)
        {
            return new QuestionnairePresenter(questionnaire);
        }
	}
}
