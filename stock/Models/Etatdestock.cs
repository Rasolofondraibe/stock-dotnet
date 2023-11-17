using Npgsql;
using System.Data;
namespace stock.Models;

class Etatdestock{
    String Idmouvement;
    String Date;
    String Idarticle;
    double Quantitereste;
    double  Moyenneprixunitaire;
    String Idmagasin;
    String Nomarticle;
    String Nommagasin;

    public Etatdestock(){} 

    public Etatdestock(String idmouvement,String date,String idarticle,double quantitereste,double moyenneprixunitaire,String idmagasin,String nomarticle,String nommagasin){
        this.Idmouvement = idmouvement;
        this.Date = date;
        this.Idarticle = idarticle;
        this.Quantitereste = quantitereste;
        this.Moyenneprixunitaire = moyenneprixunitaire;
        this.Idmagasin = idmagasin;
        this.Nomarticle = nomarticle;
        this.Nommagasin = nommagasin;
    }  

    public String getIdmouvement(){
        return this.Idmouvement;
    }

    public void setIdmouvement(String idmouvement){
        this.Idmouvement = idmouvement;
    }

    public String getDate(){
        return this.Date;
    }

    public void setDate(String date){
        this.Date = date;
    }

    public String getIdarticle(){
        return this.Idarticle;
    }

    public void setIdarticle(String idarticle){
        this.Idarticle = idarticle;
    }

    public double getQuantitereste(){
        return this.Quantitereste;
    }

    public void setQuantitereste(double quantitereste){
        this.Quantitereste = quantitereste;
    }

    public double getMoyenneprixunitaire(){
        return this.Moyenneprixunitaire;
    }

    public void setMoyenneprixunitaire(double moyenneprixunitaire){
        this.Moyenneprixunitaire = moyenneprixunitaire;
    }

    public String getIdmagasin(){
        return this.Idmagasin;
    }

    public void setIdmagasin(String idmagasin){
        this.Idmagasin = idmagasin;
    }

    public String getNomarticle(){
        return this.Nomarticle;
    }

    public void setNomarticle(String nomarticle){
        this.Nomarticle = nomarticle;
    }

    public String getNommagasin(){
        return this.Nommagasin;
    }

    public void setNommagasin(String nommagasin){
        this.Nommagasin = nommagasin;
    }


    public void getetatdestockbydate(String date,Article article,String idmagasin,NpgsqlConnection liaisonbase){
        
        String sql = "SELECT * FROM etatdestock WHERE date <= @date AND idarticle = @idarticle AND idmagasin = @idmagasin ORDER BY date DESC LIMIT 1";
        if(liaisonbase == null || liaisonbase.State == ConnectionState.Closed){
            Connexion connexion = new Connexion ();
            liaisonbase = connexion.createLiaisonBase();
            liaisonbase.Open();
        }
        try{
            NpgsqlCommand cmd = new NpgsqlCommand(sql, liaisonbase);
            cmd.Parameters.AddWithValue("@date", DateTime.Parse(date));
            cmd.Parameters.AddWithValue("@idarticle", article.getIdarticle());
            cmd.Parameters.AddWithValue("@idmagasin", idmagasin);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()){
               this.setIdmouvement(reader.GetString(0));
               this.setDate(reader.GetDateTime(1).ToString());
               this.setIdarticle(reader.GetString(2));
               this.setQuantitereste(reader.GetDouble(3));
               this.setMoyenneprixunitaire(reader.GetDouble(4));
               this.setIdmagasin(reader.GetString(5));
               this.setNomarticle(reader.GetString(6));
               this.setNommagasin(reader.GetString(7));
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }finally{
            if(liaisonbase != null){
                liaisonbase.Close();
            }
        }
    }

    public List<Etatdestock> listeetatdestocksouscategorie(String date,String idmagasin,String referenceproduit,NpgsqlConnection liaisonbase){
        Article article = new Article();
        List<Article> listearticle = article.listesouscategoriearticle(referenceproduit,liaisonbase);
        List<Etatdestock> listeetatdestock = new List<Etatdestock>();
        foreach (Article a in listearticle)
        {   
            Etatdestock etatdestock = new Etatdestock();
            etatdestock.getetatdestockbydate(date,a,idmagasin,liaisonbase);
            if(etatdestock.getIdmouvement() != null){
                listeetatdestock.Add(etatdestock);
            }
        }
        return listeetatdestock;
    }

    public double calculmontant(){
        return this.getQuantitereste() * this.getMoyenneprixunitaire();
    }

    public Article getarticleetatdestock(){
        Article article = new Article();
         article.getarticlebyidarticle(this.getIdarticle(),null);
         return article;
    }

    public double sommemontantetatdestock(String date,String idmagasin,String referenceproduit,NpgsqlConnection liaisonbase){
        List<Etatdestock> listeetatdestock = this.listeetatdestocksouscategorie(date,idmagasin,referenceproduit,liaisonbase);
        double montant = 0;
        foreach(Etatdestock etatdestock in listeetatdestock){
            montant = montant+etatdestock.calculmontant();
        }   
        return montant;
    }
}