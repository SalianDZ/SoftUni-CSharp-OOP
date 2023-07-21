using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private SubjectRepository subjects;
        private StudentRepository students;
        private UniversityRepository universities;

        public Controller()
        {
            subjects = new();
            students = new();
            universities = new();
        }
        public string AddStudent(string firstName, string lastName)
        {
            string resultMessage;
            if (students.Models.Any(x => x.FirstName == firstName && x.LastName == lastName))
            {
                resultMessage = String.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
                throw new ArgumentException(resultMessage);
            }

            int currentId = 1;

            if (students.Models.Count > 0)
            {
                currentId = students.Models.Count + 1;
            }

            IStudent student = new Student(currentId, firstName, lastName);
            students.AddModel(student);
            resultMessage = String.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, students.GetType().Name);
            return resultMessage;
        } // done

        public string AddSubject(string subjectName, string subjectType)
        {
            int currentId = 1;
            string resultMessage;

            ISubject result;
            if (subjects.Models.Count > 0)
            {
                currentId = subjects.Models.Count + 1;
            }

            if (subjectType == "TechnicalSubject")
            {
                result = new TechnicalSubject(currentId, subjectName);
            }
            else if (subjectType == "EconomicalSubject")
            {
                result = new EconomicalSubject(currentId, subjectName);
            }
            else if (subjectType == "HumanitySubject")
            {
                result = new HumanitySubject(currentId, subjectName);
            }
            else
            {
                resultMessage = String.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
                throw new ArgumentException(resultMessage);
            }

            if (subjects.Models.Any(x => x.Name == subjectName))
            {
                resultMessage = String.Format(OutputMessages.AlreadyAddedSubject, subjectName);
                throw new ArgumentException(resultMessage);
            }

            resultMessage = String.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, subjects.GetType().Name);
            subjects.AddModel(result);
            return resultMessage;
        } //done

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            string resultMessage;
            if (universities.Models.Any(x => x.Name == universityName))
            {
                resultMessage = String.Format(OutputMessages.AlreadyAddedUniversity, universityName);
                throw new ArgumentException(resultMessage);
            }

            List<int> requiredSubjectsIds = new();
            foreach (var subject in requiredSubjects)
            {
                requiredSubjectsIds.Add(subjects.Models.FirstOrDefault(x => x.Name == subject).Id);
            }

            int currentId = 1;
            if (universities.Models.Count > 0)
            {
                currentId = universities.Models.Count + 1;
            }

            IUniversity university = new University(currentId, universityName, category, capacity, requiredSubjectsIds);
            universities.AddModel(university);
            resultMessage = String.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universities.GetType().Name);
            return resultMessage;
        }//done

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] fullName = studentName.Split(" ");
            string firstName = fullName[0];
            string lastName = fullName[1];
            string resultMessage;

            if (!students.Models.Any(x => x.FirstName == firstName && x.LastName == lastName))
            {
                resultMessage = String.Format(OutputMessages.StudentNotRegitered, firstName, lastName);
                throw new ArgumentException(resultMessage);
            }

            if (!universities.Models.Any(x => x.Name == universityName))
            {
                resultMessage = String.Format(OutputMessages.UniversityNotRegitered, universityName);
                throw new ArgumentException(resultMessage);
            }

            IStudent student = students.FindByName(studentName);
            IUniversity university = universities.FindByName(universityName);

            foreach (var universityExam in university.RequiredSubjects)
            {
                if (!student.CoveredExams.Contains(universityExam))
                {
                    resultMessage = String.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
                    throw new ArgumentException(resultMessage);
                }
            }

            if (student.University == university)
            {
                resultMessage = String.Format(OutputMessages.StudentAlreadyJoined, firstName, lastName, universityName);
                throw new ArgumentException(resultMessage);
            }

            student.JoinUniversity(university);
            resultMessage = String.Format(OutputMessages.StudentSuccessfullyJoined, firstName, lastName, universityName);
            return resultMessage;
        } // done

        public string TakeExam(int studentId, int subjectId)
        {
            string resultMessage;
            IStudent student = students.FindById(studentId);
            if (student == null)
            {
                resultMessage = String.Format(OutputMessages.InvalidStudentId);    
                throw new ArgumentException(resultMessage);
            }

            ISubject subject = subjects.FindById(subjectId);

            if (subject == null)
            {
                resultMessage = String.Format(OutputMessages.InvalidSubjectId);
                throw new ArgumentException(resultMessage);
            }

            if (student.CoveredExams.Contains(subjectId))
            {
                resultMessage = String.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
                throw new ArgumentException(resultMessage);
            }

            student.CoverExam(subject);
            resultMessage = String.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
            return resultMessage;
        } // done

        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);
            int studentCount = students.Models.Count(x => x.University == university);
            StringBuilder sb = new();
            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {studentCount}");
            sb.AppendLine($"University vacancy: {university.Capacity - studentCount}");

            return sb.ToString().TrimEnd();
        }
    }
}
