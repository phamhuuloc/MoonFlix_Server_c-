using MovieServer.Models;
using MySql.Data.MySqlClient;

namespace MovieServer.Services
{
    public class movieServices
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public movieServices(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        // CREATE A NEW USER (SIGN UP)
        public int createNewMovie(Movie movie)
        {

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "insert into movie  (supplier_id ,title,_desc,img,imgSm,trailer,video,year,_limit,price,clicked,isSeries) values(@supplier_id ,@title,@_desc,@img,@imgSm,@trailer,@video,@year,@_limit,@price,@clicked,@isSeries)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("supplier_id", movie.supplier_id);
                cmd.Parameters.AddWithValue("title", movie.title);
                cmd.Parameters.AddWithValue("_desc", movie._desc);
                cmd.Parameters.AddWithValue("img", movie.img);
                cmd.Parameters.AddWithValue("imgSm",movie.imgSm);
                cmd.Parameters.AddWithValue("trailer", movie.trailer);
                cmd.Parameters.AddWithValue("video", movie.video);
                cmd.Parameters.AddWithValue("year", movie.year);
                cmd.Parameters.AddWithValue("_limit", movie._limit);
                cmd.Parameters.AddWithValue("price", movie.price);
                cmd.Parameters.AddWithValue("clicked", movie.clicked);
                cmd.Parameters.AddWithValue("isSeries", movie.isSeries);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int updateMovie(Movie movie, string id)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update movie set supplier_id = @supplier_id, title = @title, _desc = @_desc , img = @img, imgSm = @imgSm , trailer = @trailer , year = @year , _limit = @_limit , price = @price , clicked = @clicked ,isSeries = @isSeries where id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", movie.id);
                cmd.Parameters.AddWithValue("supplier_id", movie.supplier_id);
                cmd.Parameters.AddWithValue("title", movie.title);
                cmd.Parameters.AddWithValue("_desc", movie._desc);
                cmd.Parameters.AddWithValue("img", movie.img);
                cmd.Parameters.AddWithValue("imgSm", movie.imgSm);
                cmd.Parameters.AddWithValue("trailer", movie.trailer);
                cmd.Parameters.AddWithValue("video", movie.video);
                cmd.Parameters.AddWithValue("year", movie.year);
                cmd.Parameters.AddWithValue("_limit", movie._limit);
                cmd.Parameters.AddWithValue("price", movie.price);
                cmd.Parameters.AddWithValue("clicked", movie.clicked);
                cmd.Parameters.AddWithValue("isSeries", movie.isSeries);
                return (cmd.ExecuteNonQuery());
            }
        }
        // DELETE USER 
        public int deleteMovie(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from movie where id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                return (cmd.ExecuteNonQuery());
            }

        }
        // GET LIST ALL Movie
        public List<Movie> getAllMovies()
        {
            List<Movie> list = new List<Movie>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from movie";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Movie()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            supplier_id = Convert.ToInt32(reader["supplier_id"]),
                            title = reader["title"].ToString(),
                            _desc = reader["_desc"].ToString(),
                            img = reader["img"].ToString(),
                            imgSm = reader["imgSm"].ToString(),
                            trailer = reader["trailer"].ToString(),
                            video = reader["video"].ToString(),
                            year = Convert.ToInt32(reader["year"]),
                            _limit = Convert.ToDouble(reader["_limit"]),
                            price = Convert.ToDouble(reader["price"]),
                            clicked = Convert.ToInt32(reader["clicked"]),
                            isSeries = Convert.ToBoolean(reader["isSeries"]),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // GET Movie BY ID 
        public Movie getMovie(int id)
        {
            Movie movie = new Movie();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from movie where id =@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    movie.id = Convert.ToInt32(reader["id"]);
                    movie.supplier_id = Convert.ToInt32(reader["supplier_id"]);
                    movie.title = reader["title"].ToString();
                    movie._desc = reader["_desc"].ToString();
                    movie.img = reader["img"].ToString();
                    movie.imgSm = reader["imgSm"].ToString();
                    movie.trailer = reader["trailer"].ToString();
                    movie.video = reader["video"].ToString();
                    movie.year = Convert.ToInt32(reader["year"]);
                    movie._limit = Convert.ToDouble(reader["_limit"]);
                    movie.price = Convert.ToDouble(reader["price"]);
                    movie.clicked = Convert.ToInt32(reader["clicked"]);
                    movie.isSeries = Convert.ToBoolean(reader["isSeries"]);
                }

            }
            return (movie);
        }

        //GET TOP 10 MOVIE  
        public List<Movie> getTopMovie()
        {
            List<Movie> list = new List<Movie>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * , COUNT(um.um_movie_id) as 'amount' from movie  INNER JOIN user_movies um on um.um_movie_id = movie.id GROUP BY um.um_movie_id  ORDER BY count(um.um_movie_id)  DESC LIMIT 10;";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Movie()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            supplier_id = Convert.ToInt32(reader["supplier_id"]),
                            title = reader["title"].ToString(),
                            _desc = reader["_desc"].ToString(),
                            img = reader["img"].ToString(),
                            imgSm = reader["imgSm"].ToString(),
                            trailer = reader["trailer"].ToString(),
                            video = reader["video"].ToString(),
                            year = Convert.ToInt32(reader["year"]),
                            _limit = Convert.ToDouble(reader["_limit"]),
                            price = Convert.ToDouble(reader["price"]),
                            clicked = Convert.ToInt32(reader["clicked"]),
                            isSeries = Convert.ToBoolean(reader["isSeries"]),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // GET REVENUE OF MOVIE WITH ID 
        public object getRevenueOfMovie(int id)
        {
           

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT movie.title as 'movie_name',  COUNT(um.um_movie_id) as 'amount', SUM(movie.price) AS 'revenue' from movie  INNER JOIN user_movies um on um.um_movie_id = movie.id WHERE movie.id = @id  GROUP BY um.um_movie_id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                //var data = cmd.ExecuteScalar();
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    var ob = new { movie_name = reader["movie_name"].ToString(), amount = Convert.ToInt32(reader["amount"]), revenue = Convert.ToDouble(reader["revenue"]) };
                    reader.Close();
                    return ob;
                }
                conn.Close();

            }

        }
        public List<Categorie> getCategorieOfMovie(int id)
        {
            List<Categorie> list = new List<Categorie>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT *  from categories  cate INNER JOIN movie_categoties mc ON cate.id =  mc.mv_cate_id  WHERE mc.mv_movie_id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Categorie()
                        {
                            id = Convert.ToInt32(reader["id"].ToString()),
                            cate_name = reader["cate_name"].ToString(),
                            cate_type = Convert.ToInt32(reader["cate_type"]),
                        });
                    }
                    reader.Close();

                }
            }
            return (list);

        }
        //public List<Movie> getCategorieOfMovie(int id)
        //{
        //    List<Movie> list = new List<Movie>();
        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();
        //        var str = "SELECT *  FROM  movie   INNER JOIN user_movies um ON um.id = movie.id where um.um_user_id = @id";
        //        MySqlCommand cmd = new MySqlCommand(str, conn);
        //        cmd.Parameters.AddWithValue("id", id);
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                list.Add(new Movie()
        //                {
        //                    id = Convert.ToInt32(reader["id"].ToString()),
        //                    supplier_id = Convert.ToInt32(reader["supplier_id"]),
        //                    title = reader["title"].ToString(),
        //                    _desc = reader["_desc"].ToString(),
        //                    img = reader["img"].ToString(),
        //                    imgSm = reader["imgSm"].ToString(),
        //                    trailer = reader["trailer"].ToString(),
        //                    video = reader["video"].ToString(),
        //                    year = Convert.ToInt32(reader["year"]),
        //                    _limit = Convert.ToInt32(reader["_limit"]),
        //                    price = Convert.ToDouble(reader["price"]),
        //                    clicked = Convert.ToInt32(reader["clicked"]),
        //                    isSeries = Convert.ToBoolean(reader["isSeries"])
        //                });
        //            }
        //            reader.Close();

        //        }
        //    }
        //    return (list);

        //}

    }
}
