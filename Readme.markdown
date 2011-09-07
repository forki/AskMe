## What is AskMeItems

AskMeItems is a little questionnaire presenter and can be used for scientific studies. AskMeItems allows to specify a subject code and a selection of questionnaires to be presented.

## Prerequisites

* You need to install the Microsoft .NET Framework 4 - http://www.microsoft.com/download/en/details.aspx?id=17718

## Instructions

### Creating a questionnaire

* Add a new questionnaire by creating a .txt file and writing down all questions, answers and points in just one file.
* The file name should be the name of the questionnaire. This is important since the results are saved with the questionnaire as a prefix.
* If you want to present the answers horizontally, than type: "Questionnaire-Type: Likert" in the very first line of the .txt file.
    Otherwise the answers will be presented one below the other.

### Adding an introduction

* Start with "Introduction: ..." and then type the introductory message.
* AskMeItem will show the Introduction on the first page of each questionnaire

### Adding questions (or so called "items")

* Create questions by typing the questionnaire name and a number, combined with an "_", e.g. 

         ABC_1: Hello, how are you?
         
         ABC_2: How do you do?

* You can create subscales X and Y of a questionnaire ABC by typing ABC-X_1, ABC-X_2, ABC-Y_1, ABC-Y_2.
* AskMeItems will calculate the mean and sum of each subscale separately and the mean and sum of the complete questionnaire.
* Mark "fill items" with an * . These items will be shown but they won't be recognized when calculating the results e.g. 

         * ABC_2: Hello, how are you? 
           1) :) - 2   (will give 2 points)
           2) :( - 1   (will give 1 point)

* Create as many answers as you need by typing each in a new line.
* Give points by typing them behind the answers, e.g. 

         ABC_2: Hello, how are you? 
           1) :) - 2   (will give 2 points)
           2) :( - 1   (will give 1 point)

* If you don't specify points, AskMeItems will take the number of the answers as the points, e.g. 

         ABC_2: Hello, how are you? 
           1) :)       (will give 1 point)
           2) :(       (will give 2 points)

### Using comments

* It is possible to use comments in the questionnaires. Just use a # sign at the line start. e.g.

         ABC_2: Hello, how are you? 
         # This line is a comment and will not be displayed.
           1) :)       (will give 1 point)
           2) :(       (will give 2 points)


### Running a series of questionnaires

* AskMeItems stops whenever the last answer of the very last questionnaire is given.
* You can manually stop AskMe by pressing Alt+F4
* AskMeItem saves automatically 2 results files:
  * one containing only the given answers and corresponding points
  * and one containing additional information about the sum and mean of subscales and the whole quesionnaire.