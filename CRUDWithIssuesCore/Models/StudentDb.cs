using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWithIssuesCore.Models
{
    public static class StudentDb
    {
        public static async Task<Student> Add(Student p, SchoolContext db)
        {
            //Add student to context
            await db.AddAsync(p);
            await db.SaveChangesAsync();
            return p;
        }
        //
        public static async Task<List<Student>> GetStudents(SchoolContext context)
        {
            List<Student> students = await (from s in context.Students
                                            select s).ToListAsync();
            return students;
        }

        public static async Task<Student> GetStudent(SchoolContext context, int id)
        {
            Student p2 = await (from s in context.Students
                                where s.StudentId == id
                                select s).SingleOrDefaultAsync();
            return p2;
        }

        public static async Task Delete(SchoolContext context, Student p)
        {
            //Student newStudent = new Student();
            context.Entry(p).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public static async Task<Student> Update(SchoolContext context, Student p)
        {
            //Mark the object as deleted
            context.Update(p);

            //Send delete query to database
            await context.SaveChangesAsync();
            return p;
        }
    }
}