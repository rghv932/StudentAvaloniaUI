using Newtonsoft.Json;
using ReactiveUI;
using StudentAvaloniaUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;

namespace StudentAvaloniaUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region fields
        public int id=0;
        public string name;
        public string roll;
        public string labelMessage;
        private HttpClient client = new();
        public ObservableCollection<Student> students;
        #endregion

        #region ctor
        public MainWindowViewModel()
        {
            EditCommand = ReactiveCommand.Create(btnEditStudent);
            client.BaseAddress = new Uri("https://localhost:44375/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
            GetStudents();
            
        }
        #endregion

        #region Properties
        public string Greeting => "Welcome to Avalonia!";

        public int Id
        {
            get => id;
            set => this.RaiseAndSetIfChanged(ref id, value);
        }

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public string Roll
        {
            get => roll;
            set => this.RaiseAndSetIfChanged(ref roll, value);
        }

        public string LabelMessage
        {
            get => labelMessage;
            set => this.RaiseAndSetIfChanged(ref labelMessage, value);
        }

        
        public ObservableCollection<Student> Students
        {
            get => students;
            set => this.RaiseAndSetIfChanged(ref students, value);
        }

        public ICommand EditCommand { get; set; }
        #endregion

        #region CRUD
        private async void GetStudents()
        {
            LabelMessage = "";

            var response = await client.GetStringAsync("students");
            var students = JsonConvert.DeserializeObject<List<Student>>(response);
            Students = new ObservableCollection<Student>(students);
        }

        private async void SaveStudent(Student student)
        {
            await client.PostAsJsonAsync("students", student);
        }

        private async void UpdateStudent(Student student)
        {
            await client.PutAsJsonAsync("students/" + student.StudentId, student);
        }

        private async void DeleteStudent(int studentId)
        {
            await client.DeleteAsync("students/" + studentId);
        }
        #endregion

        #region Commands
        public void btnLoadStudentsClick()
        {
            this.GetStudents();
        }

        public void btnSaveStudentClick()
        {
            Student student = new()
            {
                StudentId = Convert.ToInt32(Id),
                Name = Name,
                RollNo = Roll
            };

            if (student.StudentId == 0)
            {
                //this.SaveStudent(student);
                LabelMessage = "Student Saved";
            }
            else
            {
                this.UpdateStudent(student);
                LabelMessage = "Student Updated";
            }

            Id = 0;
            Name = "";
            Roll = "";
        }

        public void btnEditStudent()
        {
            //Student student = DataContext as Student;
            //Id = student.StudentId;
            //Name = student.Name;
            //Roll = student.Roll;
        }
        #endregion
    }
}