using Avalonia.Controls;
using Avalonia.Interactivity;
using Newtonsoft.Json;
using StudentAvaloniaUI.Models;
using StudentAvaloniaUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace StudentAvaloniaUI.Views
{
    public partial class MainWindow : Window
    {
        //HttpClient client = new();
        public MainWindow()
        {
            //client.BaseAddress = new Uri("http://localhost:4400/api/");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json")
            //    );
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        //private void btnLoadStudents_Click()
        //{
        //    this.GetStudents();
        //}

        //private async void GetStudents()
        //{
        //    lblMessage.Content = "";

        //    var response = await client.GetStringAsync("students");
        //    var students = JsonConvert.DeserializeObject<List<Student>>(response);
        //    dgStudent.DataContext = students;
        //}

        //private async void SaveStudent(Student student)
        //{
        //    await client.PostAsJsonAsync("students", student);
        //}

        //private async void UpdateStudent(Student student)
        //{
        //    await client.PutAsJsonAsync("students/"+student.StudentId, student);
        //}

        //private async void DeleteStudent(int studentId)
        //{
        //    await client.DeleteAsync("students/"+ studentId);
        //}

        //public void btnSaveStudentClick()
        //{
        //    var student = new Student()
        //    {
        //        StudentId = Convert.ToInt32(txtStudentId.Text),
        //        Name = txtName.Text,
        //        Roll = txtRoll.Text
        //    };

        //    if (student.StudentId == 0)
        //    {
        //        this.SaveStudent(student);
        //        lblMessage.Content = "Student Saved";
        //    }
        //    else
        //    {
        //        this.UpdateStudent(student);
        //        lblMessage.Content = "Student Updated";
        //    }

        //    txtStudentId.Text = 0.ToString();
        //    txtName.Text = "";
        //    txtRoll.Text = "";
        //}

        //public void btnEditStudent()
        //{
        //    Student student = DataContext as Student;
        //    txtStudentId.Text = student.StudentId.ToString();
        //    txtName.Text = student.Name;
        //    txtRoll.Text = student.Roll;
        //}

        //void btnDeleteStudent(object sender, RoutedEventArgs e)
        //{
        //    Student student = DataContext as Student;
        //    this.DeleteStudent(student.StudentId);
        //}
    }
}
