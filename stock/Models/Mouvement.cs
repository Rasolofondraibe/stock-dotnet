using Npgsql;
using System.Data;

namespace stock.Models;

class Mouvement{
    String Idmouvement;
    String Date;
    String Idarticle;
    double Quantiteentree;
    double Quantitesortie;
    double Prixunitaire;
    String Idmagasin;
    double Reste;

    public Mouvement(){}

    public Mouvement(String idmouvement,String date,String idarticle,double quantiteentree,double quantitesortie,double prixunitaire,String idmagasin,double reste){
        this.Idmouvement = idmouvement;
        this.Date = date;
        this.Idarticle = idarticle;
        this.Quantiteentree = quantiteentree;
        this.Quantitesortie = quantitesortie;
        this.Prixunitaire = prixunitaire;
        this.Idmagasin = idmagasin;
        this.Reste = reste;
    }

    public Mouvement(String date,String idarticle,double quantiteentree,double quantitesortie,double prixunitaire,String idmagasin){
        this.Date = date;
        this.Idarticle = idarticle;
        this.Quantiteentree = quantiteentree;
        this.Quantitesortie = quantitesortie;
        this.Prixunitaire = prixunitaire;
        this.Idmagasin = idmagasin;
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

    public double getQuantiteentree(){
        return this.Quantiteentree;
    }

    public void setQuantiteentree(double quantiteentree){
        this.Quantiteentree = quantiteentree;
    }

    public double getQuantitesortie(){
        return this.Quantitesortie;
    }

    public void setQuantitesortie(double quantitesortie){
        this.Quantitesortie = quantitesortie;
    }

    public double getPrixunitaire(){
        return this.Prixunitaire;
    }

    public void setPrixunitaire(double prixunitaire){
        this.Prixunitaire = prixunitaire;
    }

    public String getIdmagasin(){
        return this.Idmagasin;
    }

    public void setIdmagasin(String idmagasin){
        this.Idmagasin = idmagasin;
    }

    public double getReste(){
        return this.Reste;
    }

    public void setReste(double reste){
        this.Reste = reste;
    }

    public void insertionmouvement(NpgsqlConnection liaisonbase){
        String sql = "INSERT INTO mouvement(date,idarticle,quantiteentree,quantitesortie,prixunitaire,idmagasin) VALUES (@date,@idarticle,@quantiteentree,@quantitesortie,@prixunitaire,@idmagasin) RETURNING idmouvement";
        if(liaisonbase == null || liaisonbase.State == ConnectionState.Closed){
            Connexion connexion = new Connexion ();
            liaisonbase = connexion.createLiaisonBase();
            liaisonbase.Open();
        }
        try{
            NpgsqlCommand cmd = new NpgsqlCommand(sql, liaisonbase);
            cmd.Parameters.AddWithValue("@date", DateTime.Parse(this.getDate()));
            cmd.Parameters.AddWithValue("@idarticle", this.getIdarticle());
            cmd.Parameters.AddWithValue("@quantiteentree",this.getQuantiteentree());
            cmd.Parameters.AddWithValue("@quantitesortie",this.getQuantitesortie());
            cmd.Parameters.AddWithValue("@prixunitaire",this.getPrixunitaire());
            cmd.Parameters.AddWithValue("@idmagasin",this.getIdmagasin());
            String insertedId = cmd.ExecuteScalar().ToString();
            this.setIdmouvement(insertedId);
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }finally{
            if(liaisonbase != null){
                liaisonbase.Close();
            }
        }
    }

    public List<Mouvement> getmouvementwithreste(NpgsqlConnection liaisonbase){
        Article article =  new Article();
        article.getarticlebyidarticle(this.getIdarticle(),liaisonbase);

        String sql = "";
        if(article.getType() == "fifo"){
            sql = "SELECT * FROM mouvement_reste WHERE reste>0 AND idarticle = @idarticle AND idmagasin = @idmagasin ORDER BY date";
        }else if(article.getType() == "lifo"){
            sql = "SELECT * FROM mouvement_reste WHERE reste>0 AND idarticle = @idarticle AND idmagasin = @idmagasin ORDER BY date DESC";
        }
        
        List<Mouvement> listemouvement = new List<Mouvement>();
        if(liaisonbase == null || liaisonbase.State == ConnectionState.Closed){
            Connexion connexion = new Connexion ();
            liaisonbase = connexion.createLiaisonBase();
            liaisonbase.Open();
        }
        try{
            NpgsqlCommand cmd = new NpgsqlCommand(sql, liaisonbase);
            cmd.Parameters.AddWithValue("@idarticle", this.getIdarticle());
            cmd.Parameters.AddWithValue("@idmagasin", this.getIdmagasin());
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read()){
                Mouvement mouvement =  new Mouvement(reader.GetString(0),reader.GetDateTime(1).ToString(),reader.GetString(2),reader.GetDouble(3),reader.GetDouble(4),reader.GetDouble(5),reader.GetString(6),reader.GetDouble(7));
                listemouvement.Add(mouvement);
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }finally{
            if(liaisonbase != null){
                liaisonbase.Close();
            }
        }
        return listemouvement;
    }
    public void insertionsortie(String sortie,String entree,double difference,double reste,NpgsqlConnection liaisonbase){
        String sql = "INSERT INTO sortie VALUES (@sortie,@entree,@difference,@reste)";
        if(liaisonbase == null || liaisonbase.State == ConnectionState.Closed){
            Connexion connexion = new Connexion ();
            liaisonbase = connexion.createLiaisonBase();
            liaisonbase.Open();
        }
        try{
            NpgsqlCommand cmd = new NpgsqlCommand(sql, liaisonbase);
            cmd.Parameters.AddWithValue("@sortie", sortie);
            cmd.Parameters.AddWithValue("@entree", entree);
            cmd.Parameters.AddWithValue("@difference", difference);
            cmd.Parameters.AddWithValue("@reste", reste);
            cmd.ExecuteReader();
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }finally{
            if(liaisonbase != null){
                liaisonbase.Close();
            }
        }
    }

    public void insertionsortiemouvement(NpgsqlConnection liaisonbase){
        List<Mouvement> listemouvement  = this.getmouvementwithreste(liaisonbase);
        double sortie  =  this.getQuantitesortie();
        int i= 0;
        double difference = 0;
        double reste = 0;
        while(sortie > 0 && i < listemouvement.Count){
            if(listemouvement[i].getReste() < sortie){
                difference = listemouvement[i].getReste();
                reste = 0;
                this.insertionsortie(this.getIdmouvement(),listemouvement[i].getIdmouvement(),difference,reste,liaisonbase);
                sortie = sortie - listemouvement[i].getReste();
            }else{
                difference = sortie;
                reste = listemouvement[i].getReste() - sortie;
                this.insertionsortie(this.getIdmouvement(),listemouvement[i].getIdmouvement(),difference,reste,liaisonbase);
                sortie = 0;
            }
            i++;
        }
    }

    public void todosortie(NpgsqlConnection liaisonbase){
        this.insertionmouvement(liaisonbase);
        this.insertionsortiemouvement(liaisonbase);
    }

   /* public boolean verificationstocksortie(String idarticle){
        String sql = "SELECT * FR"
    }*/

}
