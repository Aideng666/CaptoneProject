using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class QuestionReader : MonoBehaviour
{
    public List<Question> questionList { get; private set; }
    public List<Question>[] questionsByGrade { get; private set; }

    public static QuestionReader Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initializes question lists
        questionList = new List<Question>();
        questionsByGrade = new List<Question>[9];

        for (int i = 0; i < questionsByGrade.Length; i++)
        {
            questionsByGrade[i] = new List<Question>();
        } 

        ReadCSV();

        SplitQuestions();
    }

    //Reads from a CSV file to get every question from a spreadsheet and saves them into the lists
    void ReadCSV()
    {
        TextAsset file = Resources.Load("CurriculumQuestions") as TextAsset;

        string[] fileEntries = file.text.Split('\n');

        //Reads each line and seperates the values properly
        for (int i = 1; i < fileEntries.Length; i++)
        {
            string[] entrySplit = fileEntries[i].Split(',');

            string question = entrySplit[0];
            string learningTip = entrySplit[7];
            Int32.TryParse(entrySplit[1], out int grade);

            Subjects subject = Subjects.None;
            if (entrySplit[2].StartsWith("M", true, null))
            {
                subject = Subjects.Math;
            }
            else if (entrySplit[2].StartsWith("S", true, null))
            {
                subject = Subjects.Science;
            }
            else if (entrySplit[2].StartsWith("E", true, null) || entrySplit[2].StartsWith("L", true, null))
            {
                subject = Subjects.Literacy;
            }

            string correctAnswer = entrySplit[3];

            string[] wrongAnswers = new string[3];
            wrongAnswers[0] = entrySplit[4];
            wrongAnswers[1] = entrySplit[5];
            wrongAnswers[2] = entrySplit[6];

            questionList.Add(new Question(question, learningTip, grade, subject, correctAnswer, wrongAnswers));
        }
    }

    //Splits the questions into their respective grade lists
    void SplitQuestions()
    {
        for (int i = 0; i < questionsByGrade.Length; i++)
        {
            foreach (Question question in questionList)
            {
                if (question._grade == i)
                {
                    questionsByGrade[i].Add(question);

                    question.SetQuestionNum(questionsByGrade[i].Count);
                }
            }
        }
    }
}
