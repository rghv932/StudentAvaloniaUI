using Newtonsoft.Json;
using ReactiveUI;
using StudentAvaloniaUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive;
using System.Threading.Tasks;

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
            EditCommand = ReactiveCommand.Create<Student>(btnEditStudent);
            DeleteCommand = ReactiveCommand.Create<Student>(btnDeleteStudent);
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
        #endregion

        #region Commands
        public ReactiveCommand<Student, Unit> EditCommand { get; }
        public ReactiveCommand<Student,Unit> DeleteCommand { get; }
        #endregion

        #region CRUD
        private async void GetStudents()
        {
            LabelMessage = "";

            var response = await client.GetStringAsync("students");
            var students = JsonConvert.DeserializeObject<List<Student>>(response);
            Students = new ObservableCollection<Student>(students);
        }

        private async Task SaveStudent(Student student)
        {
            await client.PostAsJsonAsync("students", student);
        }

        private async Task UpdateStudent(Student student)
        {
            await client.PutAsJsonAsync("students/" + student.StudentId, student);
        }

        private async Task DeleteStudent(int studentId)
        {
            await client.DeleteAsync("students/" + studentId);
        }
        #endregion

        #region Methods
        public void btnLoadStudentsClick()
        {
            this.GetStudents();
        }

        public async void btnSaveStudentClick()
        {
            Student student = new()
            {
                StudentId = Convert.ToInt32(Id),
                Name = Name,
                RollNo = Roll
            };

            if (student.StudentId == 0)
            {
                await SaveStudent(student);
                LabelMessage = "Student Saved";
            }
            else
            {
                await UpdateStudent(student);
                LabelMessage = "Student Updated";
            }

            Id = 0;
            Name = "";
            Roll = "";
            await Task.Delay(500);
            GetStudents();
        }

        public void btnEditStudent(Student student)
        {
            Id = student.StudentId;
            Name = student.Name;
            Roll = student.RollNo;
        }

        public async void btnDeleteStudent(Student student)
        {
            await DeleteStudent(student.StudentId);
            LabelMessage = "Deleted";

            await Task.Delay(500);
            GetStudents();
        }
        #endregion
    }
}