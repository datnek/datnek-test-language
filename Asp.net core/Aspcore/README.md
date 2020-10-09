# I. Sujet
Vous souhaitez aider Datnek à informatiser un système de gestion des langues internationaux afin de repérer le niveau de langue de ses employés.

L’application devra être développé en backend avec nodejs et frontend angular. Le candidat doit faire le minimum de test unitaire et bien commenter son code.
Pour une langue choisie, il faudra préciser le niveau parlé, écrit et compréhension.
Pour le projet frontend, le candidat devra utiliser les notions suivantes : input, output, sujet/suscription, injection de dépendance.
Pour le backend, le candidat devra utiliser nodejs MVC et une base de données MySQL. Il est libre de faire les tests avec MongoDB, SQLite ou tout autre base de données en mémoire.

Quand l'utilisateur est connecter, il pourra ajouter, modifier, supprimer ou consulter le détail de la liste de ces langues.

Il n'est pas necessaire de créer 3 tables. deux suffironts pour la gestion. 
- Une table user: Pour la gestion des utilisateurs
- Une table langage: Pour la gestions des langues

Quand il clique sur détail, un modal doit souffrir et le statut du bouton close sur la barre de menu doit changer.

Quand il clique sur une langue, la page est traduite. Seul 3 langues sont obligatoires : français, anglais, néerlandais.

Le nombre de langues incrémente à chaque fois qu’il y’a une nouvelle entrée.
L’application doit être responsive.
 

# II.Les concepts 
- Test unitaire
- Séparation du code
- Injection des dépendances
- Mocking / in-memory database basé sur sqlserveur
- Sécurité jwt
