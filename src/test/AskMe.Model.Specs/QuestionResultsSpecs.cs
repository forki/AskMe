﻿using System;
using Machine.Specifications;

namespace AskMe.Model.Specs
{
    public class when_answering_a_question
    {
        private static Question Question;

        private Establish context =
            () => Question = 
                Ask.Question("How do you feel?")
                .WithAnswer("A", "good")
                .WithAnswer("B", "bad");

        private Because of = () => Result = Question.Answer("A");

        protected static Result Result;
        
        private It should_give_5_points = () => Result.Points.ShouldEqual(5);
    }
}