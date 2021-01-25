# WSMediaServer

Le WSMediaServer, est une solution permettant de gérer un serveur multimédia Plex. 
Cette solution, va permettre d'éteindre le serveur, si le temps d'inactivité de celui-ci a dépassé un certain seuil (prédéfini dans le fichier de config).

Le temps d'inactivité a été défini comme étant la période durant laquelle :  
- aucun visionnage (distant) n'est en cours   
- aucun téléchargement n'est en cours  
- aucune action n'a été effectué par les profils Windows connectés

Si l'une des 3 conditions n'est pas validée le compteur d'inactivité est remis à 0.
