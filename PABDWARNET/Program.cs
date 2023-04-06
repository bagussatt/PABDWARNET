using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace PABD3003
{
    internal class Program
    {
        static void Main(string[] args)

        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.WriteLine("Masukan User ID : ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukan Password : ");
                    string password = Console.ReadLine();
                    Console.WriteLine("Masukan Database Tujuan : ");
                    string db = Console.ReadLine();
                    Console.WriteLine("\nKetik K untuk terhubung ke database : ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn;
                                string connectionString;
                                connectionString = "Data source = VICTUSJ9Q2TAHT-\\BAGUSSATRIOVKAP;" +
                                    "initial catalog = {0}; User ID = {1}; password = {2}";

                                conn = new SqlConnection(string.Format(connectionString, db, user, password));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Menambahkan Data");
                                        Console.WriteLine("3. Ubah data");
                                        Console.WriteLine("4. hapus data");

                                        Console.WriteLine("3. Keluar");
                                        Console.WriteLine("\n Masukan Pilihan (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA MAHASISWA\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Mahasiswa\n");
                                                    Console.WriteLine("masukan id_billing : ");
                                                    string idbil = Console.ReadLine();
                                                    Console.WriteLine("Masukan Tanggal : ");
                                                    string tgl = Console.ReadLine();
                                                    Console.WriteLine("masukan ID OP :");
                                                    string idopr = Console.ReadLine();
                                                    Console.WriteLine("Masukan Biaya)");
                                                    string bia = Console.ReadLine();
                                                    Console.WriteLine("Masukan Jenis Login : ");
                                                    string jl = Console.ReadLine();
                                                    Console.WriteLine("Masukan Laporan AKhir : ");
                                                    string lpr = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(idbil, tgl, idopr, bia, jl, lpr, conn);
                                                        conn.Close();
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\n Anda tidak memiliki " + "akses untuk menambahkan data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\n Invalid Options");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nChech for the value entered");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid Options");

                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Tidak Dapat mengakses database menggunakan user tersebut\n");
                    Console.ResetColor();
                }
            }
        }
        public void baca(SqlConnection con)
        {
            SqlDataAdapter cmd = new SqlDataAdapter("Select * From Billing", con);
            DataSet ds = new DataSet();
            cmd.Fill(ds, "Mahasiswa");
            DataTable dt = ds.Tables["mahasiswa"];

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    Console.WriteLine(row[col]);
                }
                Console.Write("\n");
            }
        }
        public void insert(string idbil, string tanggal, string idop, string biaya, string jenislog, string laporan, SqlConnection con)
        {

            string str;
            str = "insert into Billing (Id_Billing,Tanggal,Id_OP,Biaya_Bilik,Jenis_login,Laporan_akhir)" + "values(@idbil,@tgl,@idopr,@bia,@jl,@lpr)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandText = str;

            cmd.Parameters.Add(new SqlParameter("idbil", idbil));
            cmd.Parameters.Add(new SqlParameter("tgl", tanggal));
            cmd.Parameters.Add(new SqlParameter("idopr", idop));
            cmd.Parameters.Add(new SqlParameter("bia", biaya));
            cmd.Parameters.Add(new SqlParameter("jl", jenislog));
            cmd.Parameters.Add(new SqlParameter("lpr", laporan));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");

        }

    }
}
