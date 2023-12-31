using Npgsql;
using System.Data;
namespace stock.Models;
class Article{
    String Idarticle;
    String Nomarticle;
    String Type;
    public Article(){}

    public Article(String idarticle,String nomarticle,String type){
        this.Idarticle = idarticle;
        this.Nomarticle = nomarticle;
        this.Type = type;
    }

    public String getIdarticle(){
        return this.Idarticle;
    } 

    public void setIdarticle(String Idarticle){
        this.Idarticle = Idarticle;
    }

    public String getNomarticle(){
        return this.Nomarticle;
    }

    public void setNomarticle(String nomarticle){
        this.Nomarticle = nomarticle;
    }

    public String getType(){
        return this.Type;
    }

    public void setType(String type){
        this.Type = type;
    }

    public void getarticlebyidarticle(String idarticle,NpgsqlConnection liaisonbase){
        String sql = "SELECT * FROM article WHERE idarticle = @idarticle";
        if(liaisonbase == null || liaisonbase.State == ConnectionState.Closed){
            Connexion connexion = new Connexion ();
            liaisonbase = connexion.createLiaisonBase();
            liaisonbase.Open();
        }
        try{
            NpgsqlCommand cmd = new NpgsqlCommand(sql, liaisonbase);
            cmd.Parameters.AddWithValue("@idarticle", idarticle);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()){
                this.setIdarticle(reader.GetString(0));
                this.setNomarticle(reader.GetString(1));
                this.setType(reader.GetString(2));
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }finally{
            if(liaisonbase != null){
                liaisonbase.Close();
            }
        }
    }

    public List<Article> listesouscategoriearticle(String idarticle,NpgsqlConnection liaisonbase){
        String sql = "SELECT * FROM article WHERE idarticle LIKE @idarticle || '%'";
        List<Article> listearticle = new List<Article>();
        if(liaisonbase == null || liaisonbase.State == ConnectionState.Closed){
            Connexion connexion = new Connexion ();
            liaisonbase = connexion.createLiaisonBase();
            liaisonbase.Open();
        }

        try{
            NpgsqlCommand cmd = new NpgsqlCommand(sql, liaisonbase);
            cmd.Parameters.AddWithValue("@idarticle", idarticle);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read()){
                Article article = new Article(reader.GetString(0),reader.GetString(1),reader.GetString(2));
                listearticle.Add(article);
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }finally{
            if(liaisonbase != null){
                liaisonbase.Close();
            }
        }
        return listearticle;
    }
}