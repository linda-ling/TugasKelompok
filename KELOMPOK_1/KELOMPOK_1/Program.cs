using System;
using System.Collections.Generic;
using BC = BCrypt.Net;

namespace KELOMPOK_1
{
    class Program
    {

        static void CreateUser(List<User> a)
        {
            Random rm = new Random();
            Console.Clear();
            Console.WriteLine("======================");
            Console.Write("FIRSTNAME: ");
            string depan = Console.ReadLine();
            Console.Write("LASTNAME : ");
            string belakang = Console.ReadLine();
            string pass = CekPass();
            string id;
            string random = Convert.ToString(rm.Next(0, 100));

            id = string.Concat(depan.Substring(0, 2), belakang.Substring(0, 2), random)
                .ToLower();
            a.Add(new User() { FirstName = depan, LastName = belakang, Password = BC.BCrypt.HashString(pass), UserId = id });
            Console.WriteLine("======================");
            Console.WriteLine("DATA BERHASIL DI INPUT !!!");
            Console.ReadKey(true);
        }
        static void ShowUser(List<User> a)
        {
            string nama;
            Console.Clear();
            Console.WriteLine("==========SHOW USER==========");
            foreach (User b in a)
            {
                nama = string.Concat(b.FirstName, " ", b.LastName);
                Console.WriteLine("NAMA : " + nama);
                Console.WriteLine("USERNAME : " + b.UserId);
                Console.WriteLine("PASSWORD : " + b.Password);
                Console.WriteLine("=============================");
            }
            Console.ReadKey(true);
        }
        static void SearchUser(List<User> a)
        {
            Console.Clear();
            Console.WriteLine("======================");
            Console.Write("MASUKKAN KATA KUNCI : ");
            string id = Console.ReadLine();
            string nama;
            string sPattern = id;
            int kon = 0;
            foreach (User s in a)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(string.Concat(s.FirstName, s.LastName), sPattern))
                {
                    nama = string.Concat(s.FirstName, " ", s.LastName);
                    Console.WriteLine("<< USER DITEMUKAN >>");
                    Console.WriteLine("=============================");
                    Console.WriteLine("NAMA : " + nama);
                    Console.WriteLine("USERNAME : " + s.UserId);
                    Console.WriteLine("PASSWORD : " + s.Password);
                    Console.WriteLine("=============================");
                    kon += 1;
                }
            }
            if (kon == 0)
            {
                Console.WriteLine("USER TIDAK DITEMUKAN");
            }
            //foreach (User b in a)
            //{
            //    if (id == b.UserId)
            //    {
            //        nama = string.Concat(b.FirstName, " ", b.LastName);
            //        Console.WriteLine("<< USER DITEMUKAN >>");
            //        Console.WriteLine("=============================");
            //        Console.WriteLine("NAMA : " + nama);
            //        Console.WriteLine("USERNAME : " + b.UserId);
            //        Console.WriteLine("PASSWORD : " + b.Password);
            //        Console.WriteLine("=============================");
            //    }
            //    else {
            //        Console.WriteLine("USER TIDAK ADA !!!");
            //    }
            //}
            Console.ReadKey();
        }
        static void LoginUser(List<User> a)
        {
            Console.Clear();
            Console.WriteLine("===LOGIN===");
            Console.Write("USERNAME : ");
            string id = Console.ReadLine();
            Console.Write("PASSWORD : ");
            string pass = Console.ReadLine();
            bool log = true;

            foreach (User b in a)
            {
                if (id == b.UserId && BC.BCrypt.Verify(pass, b.Password))
                {
                    Console.WriteLine(" << SELAMAT ANDA TELAH LOGIN >>");
                    switch (menu2())
                    {
                        case 1:
                            UpdateUser(a, id);
                            break;
                        case 2:
                            DeleteUser(a, id);
                            break;
                    }
                    log = true;
                    break;
                }
                else
                {
                    log = false;
                }
            }
            if (log == false)
            {
                Console.WriteLine("ID ATAU PASSWORD SALAH !!!");
            }
            Console.ReadKey(true);
        }
        //static string Base64Decode(string base64EncodedData)
        //{
        //    var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        //    return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        //}
        //static string Base64Encode(string plainText)
        //{
        //    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        //    return System.Convert.ToBase64String(plainTextBytes);
        //}
        static string CekPass()
        {
            int temp;
            bool num = false;
            Console.Write("PASSWORD : ");
            string nama = Console.ReadLine();
            num = int.TryParse(nama, out temp);
            while (num = false || nama.Length < 8)
            {
                if (num = false || nama.Length < 8)
                {
                    Console.WriteLine("INPUTAN PASSWORD SALAH !!!");
                    Console.WriteLine("* Harus Mengandung Angka dan Panjang Karakter 8");
                    Console.WriteLine("====================");
                    Console.Write("PASSWORD : ");
                    nama = Console.ReadLine();
                    num = int.TryParse(nama, out temp);
                }
            }
            return nama;
        }
        static void DeleteUser(List<User> a, string id)
        {
            bool log = false;
            foreach (User b in a)
            {
                if (id == b.UserId)
                {
                    log = true;
                    a.Remove(b);
                    Console.Clear();
                    Console.WriteLine("=======================");
                    Console.WriteLine("DATA BERHASIL DI DELETE");
                    Console.WriteLine("=======================");
                    break;
                }
                else if (log == false)
                {
                    Console.WriteLine("USER ID ATAU PASSWROD SALAH !!!");
                }
            }
            Console.ReadKey(true);
        }
        static void UpdateUser(List<User> a, string id)
        {
            Console.Clear();
            Console.WriteLine("======UPDATE USER=======");
            Console.WriteLine("1. UPDATE NAMA");
            Console.WriteLine("2. UPDATE PASWORD");
            Console.Write(">> ");
            int menu = int.Parse(Console.ReadLine());
            switch (menu)
            {
                case 1:
                    foreach (User b in a)
                    {
                        if (id == b.UserId)
                        {
                            Console.Clear();
                            Console.WriteLine("=======UPDATE NAMA=======");
                            Console.WriteLine("FIRSTNAME : " + b.FirstName);
                            Console.WriteLine("LASTNAME : " + b.LastName);
                            Console.WriteLine("=========================");
                            Console.Write("FIRSTNAME BARU : ");
                            b.FirstName = Console.ReadLine();
                            Console.Write("LASTNAME BARU : ");
                            b.LastName = Console.ReadLine();
                            b.UserId = b.FirstName.Substring(0, 2) + "" + b.LastName.Substring(0, 2);
                        }
                    }
                    break;
                case 2:
                    foreach (User b in a)
                    {
                        if (id == b.UserId)
                        {
                            Console.Clear();
                            Console.WriteLine("======UPDATE PASSWORD======");
                            Console.WriteLine("PASSWORD : " + b.Password);
                            Console.WriteLine("==========NEW PASS==========");
                            b.Password = CekPass();
                        }
                    }
                    break;
            }
        }
        static int menu2()
        {
            int menu = 0;
            Console.Clear();
            Console.WriteLine("==USER UPDATE==");
            Console.WriteLine("1. UPDATE USER");
            Console.WriteLine("2. DELETE USER");
            Console.Write("PILIH MENU >> ");
            menu = int.Parse(Console.ReadLine());
            return menu;
        }
        static void Main(string[] args)
        {
            int menu = 0;
            List<User> user = new List<User> { };
            while (menu != 5)
            {
                Console.Clear();
                Console.WriteLine("==BASIC AUNTENTIFICATION==");
                Console.WriteLine("1. ADD USER");
                Console.WriteLine("2. SHOW USER");
                Console.WriteLine("3. SEARCH");
                Console.WriteLine("4. LOGIN");
                Console.WriteLine("5. EXIT");
                Console.Write("PILIH MENU >> ");
                try
                {
                    menu = int.Parse(Console.ReadLine());
                    switch (menu)
                    {
                        case 1:
                            CreateUser(user);
                            break;
                        case 2:
                            ShowUser(user);
                            break;
                        case 3:
                            SearchUser(user);
                            break;
                        case 4:
                            LoginUser(user);
                            break;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }
}

