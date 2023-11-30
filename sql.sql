--                                                                   Tables --                                                                  

CREATE TABLE Employes (
    matricule VARCHAR(10) PRIMARY KEY NOT NULL,
    nom VARCHAR(255) NOT NULL,
    prenom VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    dateNaissance DATE NOT NULL,
    adresse VARCHAR(255) NOT NULL,
    dateEmbauche DATE NOT NULL,
    tauxHoraire DOUBLE(16,2) NOT NULL,
    photo VARCHAR(60000) NOT NULL,
    statut VARCHAR(255) NOT NULL DEFAULT 'Journalier'
);

CREATE TABLE Clients (
    id int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    nom VARCHAR(255) NOT NULL,
    adresse VARCHAR(255) NOT NULL,
    numTelephone VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL
);

ALTER TABLE Clients AUTO_INCREMENT=100;

CREATE TABLE Projets (
    num VARCHAR(11) PRIMARY KEY NOT NULL,
    titre VARCHAR(255) NOT NULL,
    dateDebut DATE NOT NULL,
    description VARCHAR(10000) NOT NULL,
    budget DOUBLE(16,2) NOT NULL,
    nbEmploye int NOT NULL,
    totalSalaire DOUBLE(16,2) NOT NULL DEFAULT 0,
    idClient int NOT NULL,
    statut VARCHAR(255) NOT NULL DEFAULT 'En cours',
    FOREIGN KEY (idClient) REFERENCES Clients (id)
);

CREATE TABLE EmployesProjets (
    numProjet VARCHAR(11) NOT NULL,
    matriculeEmploye VARCHAR(10) NOT NULL,
    nbHeures DOUBLE(16,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (numProjet) REFERENCES Projets (num),
    FOREIGN KEY (matriculeEmploye) REFERENCES Employes (matricule),
    PRIMARY KEY (numProjet, matriculeEmploye)
);

CREATE TABLE user (
    id INT DEFAULT 1,
    name VARCHAR(255),
    password VARCHAR(255),
    loged bit DEFAULT 1
);

--                                                                   Triggers --                                                                  

DELIMITER //
CREATE TRIGGER BeforeInsertEmployes
BEFORE INSERT ON Employes
FOR EACH ROW
BEGIN
  SET NEW.Matricule = CONCAT(LEFT(NEW.nom,1),LEFT(NEW.prenom,1),'-',YEAR(NEW.dateNaissance),'-',FLOOR(RAND() * (90) + 10));
END;
//
DELIMITER ;

DELIMITER //
CREATE TRIGGER BeforeInsertProjets
BEFORE INSERT ON Projets
FOR EACH ROW
BEGIN
  SET NEW.num = CONCAT(NEW.idClient,'-',FLOOR(RAND() * (90) + 10),'-',YEAR(NEW.dateDebut));
END;
//
DELIMITER ;

DELIMITER //
CREATE TRIGGER AfterInsertEmployesProjets
AFTER INSERT ON EmployesProjets
FOR EACH ROW
BEGIN
    UPDATE Projets SET totalSalaire = totalSalaire + (NEW.nbHeures * (SELECT tauxHoraire FROM Employes WHERE matricule = NEW.matriculeEmploye)) WHERE num = NEW.numProjet;
END;
//
DELIMITER ;

DELIMITER //
CREATE TRIGGER CheckAgeBeforeInsertEmployes
BEFORE INSERT ON Employes
FOR EACH ROW
BEGIN
    IF (YEAR(CURDATE()) - YEAR(NEW.dateNaissance)) < 18 OR (YEAR(CURDATE()) - YEAR(NEW.dateNaissance)) > 65 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = "L'âge doit être entre 18 et 65 ans.";
    END IF;
END;
//
DELIMITER ;

DELIMITER //
CREATE TRIGGER CheckDateEmbaucheBeforeInsertEmployes
BEFORE INSERT ON Employes
FOR EACH ROW
BEGIN
    IF NEW.dateEmbauche > CURDATE() THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La date dembauche ne peut pas être dans le futur.';
    END IF;
END;
//
DELIMITER ;

DELIMITER //
CREATE TRIGGER CheckTauxHoraireBeforeInsertEmployes
BEFORE INSERT ON Employes
FOR EACH ROW
BEGIN
    IF NEW.tauxHoraire < 0 OR NEW.tauxHoraire > 1000 OR NEW.tauxHoraire IS NULL THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Les taux horaires doivent être des valeurs numériques non négatives et raisonnablement limitées.';
    END IF;
END;
//
DELIMITER ;



--                                                                   Functions  
DELIMITER //
CREATE FUNCTION f_CheckIfEmployeWorkOnCurrentProject(_matriculeEmploye VARCHAR(255)) RETURNS VARCHAR(255)
BEGIN
    DECLARE projetName VARCHAR(255);
    SELECT P.titre INTO projetName
    FROM EmployesProjets EP
    INNER JOIN Projets P ON P.num = EP.numProjet
    WHERE matriculeEmploye = _matriculeEmploye AND P.statut = 'En cours';

    RETURN projetName;
END //
DELIMITER ;


DELIMITER //
CREATE FUNCTION f_GetClientNameById(_clientId INT) RETURNS VARCHAR(255)
BEGIN
    DECLARE clientName VARCHAR(255);

    SELECT nom INTO clientName
    FROM Clients
    WHERE id = _clientId;

    RETURN clientName;
END //
DELIMITER ;



--                                                                   Procédures --       



-- Insert
DELIMITER //
CREATE PROCEDURE InsertEmploye(
    IN _nom VARCHAR(255),
    IN _prenom VARCHAR(255),
    IN _email VARCHAR(255),
    IN _dateNaissance DATE,
    IN _adresse VARCHAR(255),
    IN _dateEmbauche DATE,
    IN _tauxHoraire DOUBLE(16,2),
    IN _photo VARCHAR(255),
    IN _statut VARCHAR(255)
)
BEGIN
    INSERT INTO Employes (nom,prenom,email,dateNaissance,adresse,dateEmbauche,tauxHoraire,photo,statut)
    VALUES (_nom,_prenom,_email,_dateNaissance,_adresse,_dateEmbauche,_tauxHoraire,_photo,_statut);
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE InsertClient (
    IN _nom VARCHAR(255),
    IN _adresse VARCHAR(255),
    IN _numTelephone VARCHAR(255),
    IN _email VARCHAR(255)
)
BEGIN
    INSERT INTO Clients (nom, adresse, numTelephone, email)
    VALUES (_nom, _adresse, _numTelephone, _email);
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE InsertProjet (
    IN _titre VARCHAR(255),
    IN _dateDebut DATE,
    IN _description VARCHAR(10000),
    IN _budget DOUBLE(16,2),
    IN _nbEmploye INT,
    IN _idClient INT
)
BEGIN
    INSERT INTO Projets (titre,dateDebut,description,budget,nbEmploye,idClient)
    VALUES (_titre,_dateDebut,_description,_budget,_nbEmploye,_idClient);
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE InsertEmployeProjet(
    IN _numProjet VARCHAR(11),
    IN _matriculeEmploye VARCHAR(10),
    IN _nbHeures DOUBLE(16,2)
)
BEGIN
    INSERT INTO EmployesProjets (numProjet, matriculeEmploye, nbHeures)
    VALUES (_numProjet, _matriculeEmploye, _nbHeures);
END //

DELIMITER ;

-- User

DELIMITER //

CREATE PROCEDURE CreateUser(
    IN _name VARCHAR(255),
    IN _password VARCHAR(255)
)
BEGIN
    INSERT INTO user (name, password)
    VALUES (_name, _password);
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE CheckIfFirstUse()
BEGIN
    DECLARE firstUse BOOLEAN;

    SELECT COUNT(*) > 0 INTO firstUse FROM user;

    SELECT firstUse AS result;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE IsUserLoggedIn()
BEGIN
    DECLARE loggedStatus BOOLEAN;

    SELECT COUNT(*) > 0 INTO loggedStatus FROM user WHERE loged=TRUE;

    SELECT loggedStatus as result;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE LoginLogout()
BEGIN
    DECLARE loggedStatus BOOLEAN;
    SELECT loged INTO loggedStatus FROM user;
    IF loggedStatus THEN
        UPDATE user set loged=FALSE WHERE id=1;
    ELSE
        UPDATE user set loged=TRUE WHERE id=1;
    END IF;
END //

DELIMITER //

CREATE PROCEDURE Connexion(
    IN _name VARCHAR(255),
    IN _password VARCHAR(255)
)
BEGIN
    DECLARE result BOOLEAN;
    SELECT COUNT(*) INTO result FROM user WHERE name = _name AND password = _password;
    SELECT result as result;
END //

DELIMITER ;


-- Update 

DELIMITER //
CREATE PROCEDURE UpdateEmployee (
    IN _matricule VARCHAR(10),
    IN _nom VARCHAR(255),
    IN _prenom VARCHAR(255),
    IN _email VARCHAR(255),
    IN _dateNaissance DATE,
    IN _adresse VARCHAR(255),
    IN _dateEmbauche DATE,
    IN _tauxHoraire DOUBLE(16,2),
    IN _photo VARCHAR(255),
    IN _statut VARCHAR(255)
)
BEGIN
    UPDATE Employes
    SET
        nom = _nom,
        prenom = _prenom,
        email = _email,
        dateNaissance = _dateNaissance,
        adresse = _adresse,
        dateEmbauche = _dateEmbauche,
        tauxHoraire = _tauxHoraire,
        photo = _photo,
        statut = _statut
    WHERE matricule = _matricule;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE UpdateClient (
    IN _id INT,
    IN _nom VARCHAR(255),
    IN _adresse VARCHAR(255),
    IN _numTelephone VARCHAR(255),
    IN _email VARCHAR(255)
)
BEGIN
    UPDATE Clients
    SET
        nom = _nom,
        adresse = _adresse,
        numTelephone = _numTelephone,
        email = _email
    WHERE id = _id;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE UpdateProject (
    IN _num VARCHAR(11),
    IN _titre VARCHAR(255),
    IN _dateDebut DATE,
    IN _description VARCHAR(10000),
    IN _budget DOUBLE(16,2),
    IN _nbEmploye INT
)
BEGIN
    UPDATE Projets
    SET
        titre = _titre,
        dateDebut = _dateDebut,
        description = _description,
        budget = _budget,
        nbEmploye = _nbEmploye
    WHERE num = _num;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE UpdateEmployeeProject (
    IN _numProjet VARCHAR(11),
    IN _matriculeEmploye VARCHAR(10),
    IN _nbHeures DOUBLE(16,2)
)
BEGIN
    UPDATE EmployesProjets
    SET nbHeures = _nbHeures
    WHERE numProjet = _numProjet AND matriculeEmploye = _matriculeEmploye;
END //
DELIMITER ;

-- Delete

DELIMITER //
CREATE PROCEDURE DeleteEmployee (
    IN _matricule VARCHAR(10)
)
BEGIN
    DELETE FROM Employes WHERE matricule = _matricule;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE DeleteClient (
    IN _id INT
)
BEGIN
    DELETE FROM Clients WHERE id = _id;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE DeleteProject (
    IN _num VARCHAR(11)
)
BEGIN
    DELETE FROM Projets WHERE num = _num;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE DeleteEmployeeProjectByEmployee (
    IN _numProjet VARCHAR(11),
    IN _matriculeEmploye VARCHAR(10)
)
BEGIN
    DELETE FROM EmployesProjets WHERE numProjet = _numProjet AND matriculeEmploye = _matriculeEmploye;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE DeleteAllEmployeeProjectByEmployee (
    IN _matriculeEmploye VARCHAR(10)
)
BEGIN
    DELETE FROM EmployesProjets WHERE matriculeEmploye = _matriculeEmploye;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE DeleteEmployeeProjectByProject (
    IN _numProjet VARCHAR(11)
)
BEGIN
    DELETE FROM EmployesProjets WHERE numProjet = _numProjet;
END //
DELIMITER ;

-- Vérification
DELIMITER //
CREATE PROCEDURE CheckIfEmployeWorkOnCurrentProject(
    IN _matriculeEmploye VARCHAR(10)
)
BEGIN
    DECLARE result VARCHAR(255);
    SELECT f_CheckIfEmployeWorkOnCurrentProject(_matriculeEmploye) INTO result;
    SELECT result AS result;
END //
DELIMITER ;


--                                                                   Données                                                                 

-- Employé
CALL InsertEmploye('Pipoco', 'Isaac', 'pipoco@hotmail.music', '2005-01-15', '123 Main St', '2022-01-01', 20, 'https://boroktimes.com/storage/2023/07/channels4_profile.jpeg', 'Permanent');
CALL InsertEmploye('crazy', 'tijay', 'tijay.amidelaforet@gmail.com', '2005-02-03', '40 rue Paul-Gauchery', '2023-01-01', 25, 'https://boroktimes.com/storage/2023/07/channels4_profile.jpeg', 'Journalier');
CALL InsertEmploye('Khaleb', 'DJ', 'wethebestmusic@hotmail.ca', '1990-01-15', 'Roblox', '2020-01-01', 30, 'https://media.nrj.fr/1900x1200/2017/06/dj-khaled_1365653.jpg', 'Permanent');
CALL InsertEmploye('Forting', 'Juju', 'jujuleplusbo@gmail.ca', '1970-01-23', 'le ciel le gros', '1999-01-01', 35, 'https://th.bing.com/th/id/OIG.6GHUZIZFLi17an.2KDWM?w=1024&h=1024&rs=1&pid=ImgDetMain', 'Permanent');
CALL InsertEmploye('Lamotte', 'Arthur', 'arthurgenrecommejtejurelegros@gmail.ca', '2005-09-12', 'salle 2255 au pfk', '1999-01-01', 90, 'https://th.bing.com/th/id/OIG.0kOxmq.VsVWGoRBMCEnl?w=1024&h=1024&rs=1&pid=ImgDetMain', 'Journalier');
CALL InsertEmploye('Asouf', 'Mouad', 'mouad@wallah.ar', '2005-09-23', '3815 rue ramadan', '2023-01-01', 7, 'https://th.bing.com/th/id/OIG.bfL6gyAiybuTCdI1zRf5?pid=ImgGn&w=1024&h=1024&rs=1', 'Journalier');
CALL InsertEmploye('Frapper blanc', 'Étienne', 'eti@gmail.com', '2005-05-12', '3310 rue Foucher', '2023-01-01', 46, 'https://th.bing.com/th/id/OIG.WhBNmXYDaUOp16O9L4hy?pid=ImgGn', 'Permanent');
CALL InsertEmploye('Rider', 'Hog', 'eti@gmail.com', '2000-01-17', '17 rue clash royale', '2022-07-11', 5, 'https://static.wikia.nocookie.net/clashofclans/images/8/84/Hog_rider.jpg/revision/latest?cb=20140711085735', 'Permanent');
CALL InsertEmploye('Doe', 'John', 'johndoe.amidelaforet@gmail.com', '2001-07-22', '643 rue ChatGpt', '2023-05-18', 17, 'https://www.blendswap.com/blend_previews/18390/0/0', 'Journalier');
CALL InsertEmploye('White', 'Walter', 'LETMECOOK@cooking.com', '2005-05-12', '3310 rue Foucher', '2023-01-01', 35, 'https://static.wikia.nocookie.net/villains/images/6/65/Walter_White2.jpg/revision/latest?cb=20230109113855', 'Permanent');

-- Clients
CALL InsertClient('Goulougoulou', '123 rue BAAAAAAAHHHHHHH', '514-555-1001', 'info@gooogle.com');
CALL InsertClient('papsi', '456 Shop Ln', '438-555-2002', 'support@papsi.com');
CALL InsertClient('robux', '789 Treat Ave', '450-555-3003', 'info@robux.com');
CALL InsertClient('MECHANT MECHANT', '101 Dino Blvd', '418-555-4004', 'contact@mechantmechant.com');
CALL InsertClient('Microhard', '202 Galaxy Dr', '450-555-5005', 'info@microhard.com');
CALL InsertClient('watah', '303 Space Blvd', '514-555-6006', 'support@watah.com');
CALL InsertClient('Elon Musk', '404 Tweet St', '438-555-7007', 'info@elonmusk.com');
CALL InsertClient('monkey association', '505 Animation Ave', '418-555-8008', 'contact@monkey.com');
CALL InsertClient('etijay corp', '606 Rock Rd', '450-555-9009', 'info@etijay.com');
CALL InsertClient('pipocos label', '707 Fizz Ave', '514-555-1010', 'contact@pipocolabel.com');

-- Projets
CALL InsertProjet('Aller manger dans un open', '2023-01-15', 'Une side quest fait par tijay', 20000, 3, 100);
CALL InsertProjet('Pogner un arbre à 200', '2023-10-30', 'Une autre side quest par tijay', 110000, 4, 102);
CALL InsertProjet('faire un nouveau album', '2023-02-20', 'gros projet de pico de bois toute le kit', 30000, 5, 101);
CALL InsertProjet('mange tout sa', '2023-03-25', 'faut manger tout sa big', 40000, 2, 103);
CALL InsertProjet('opération jus de pickle', '2023-04-30', 'Une 3e side quest de tijay feat osirion', 50000, 1, 104);
CALL InsertProjet('faut faire le ramadan', '2023-05-05', 'faut demander à mouad pour les info le gros', 60000, 4, 105);
CALL InsertProjet('une ptite vite steuplait', '2023-06-10', 'etienne est en manque !?!?!?!', 70000, 3, 106);
CALL InsertProjet('faire une game agario le gros', '2023-07-15', 'juju a besoin de teammate pour teamup big', 80000, 5, 107);
CALL InsertProjet('jveux manger un gros tacos', '2023-08-20', 'MENOUM MENOUM DES BONS BISCUITS', 90000, 2, 108);
CALL InsertProjet('faut prendre des risques dans vie le gros', '2023-09-25', 'Ah il aurait du aller, il aurait du le faire crois-moi.', 100000, 1, 109);

-- EmployéProjet
CALL InsertEmployeProjet((SELECT num FROM projets LIMIT 1),(SELECT matricule FROM employes LIMIT 1), 80);
CALL InsertEmployeProjet((SELECT num FROM projets LIMIT 1 OFFSET 1),(SELECT matricule FROM employes LIMIT 1 OFFSET 1), 45);
CALL InsertEmployeProjet((SELECT num FROM projets LIMIT 1 OFFSET 2),(SELECT matricule FROM employes LIMIT 1 OFFSET 2), 5);
CALL InsertEmployeProjet((SELECT num FROM projets LIMIT 1 OFFSET 3),(SELECT matricule FROM employes LIMIT 1 OFFSET 3), 90);
CALL InsertEmployeProjet((SELECT num FROM projets LIMIT 1 OFFSET 4),(SELECT matricule FROM employes LIMIT 1 OFFSET 4), 120);
CALL InsertEmployeProjet((SELECT num FROM projets LIMIT 1 OFFSET 5),(SELECT matricule FROM employes LIMIT 1 OFFSET 5), 155);
CALL InsertEmployeProjet((SELECT num FROM projets LIMIT 1 OFFSET 6),(SELECT matricule FROM employes LIMIT 1 OFFSET 6), 99);
CALL InsertEmployeProjet((SELECT num FROM projets LIMIT 1 OFFSET 7),(SELECT matricule FROM employes LIMIT 1 OFFSET 7), 69);
CALL InsertEmployeProjet((SELECT num FROM projets LIMIT 1 OFFSET 8),(SELECT matricule FROM employes LIMIT 1 OFFSET 8), 420);
CALL InsertEmployeProjet((SELECT num FROM projets LIMIT 1 OFFSET 9),(SELECT matricule FROM employes LIMIT 1 OFFSET 9), 76);



--                                                                   Views                                                           
-- 1. Employés avec leurs projets actuels
CREATE VIEW View_EmployesProjetsActuels AS
SELECT
    E.nom,
    E.prenom,
    P.titre AS projet_actuel,
    EP.nbHeures
FROM
    Employes E
JOIN
    EmployesProjets EP ON E.matricule = EP.matriculeEmploye
JOIN
    Projets P ON EP.numProjet = P.num;

-- 2. Liste clients avec le nombre de projets en cours
CREATE VIEW View_ListeClientsNbProjets AS
SELECT
    C.nom,
    COUNT(P.num) AS nb_projets_en_cours
FROM
    Clients C
LEFT JOIN
    Projets P ON C.id = P.idClient
WHERE
    P.statut = 'En cours'
GROUP BY
    C.nom, C.id;

-- 3. Liste projets avec le nombre total d'heures travaillées
CREATE VIEW View_ProjetsNbHeuresTotal AS
SELECT
    P.titre,
    SUM(EP.nbHeures) AS total_heures_travaillees
FROM
    Projets P
LEFT JOIN
    EmployesProjets EP ON P.num = EP.numProjet
GROUP BY
    P.titre, P.num;

-- 4. Liste des employés avec leur salaire total
CREATE VIEW View_ListeEmployesSalaireTotal AS
SELECT
    E.matricule,
    E.nom,
    E.prenom,
    SUM(EP.nbHeures * E.tauxHoraire) AS salaire_total
FROM
    Employes E
JOIN
    EmployesProjets EP ON E.matricule = EP.matriculeEmploye
GROUP BY
    E.matricule, E.nom, E.prenom;

-- 5. Liste des projets terminés
CREATE VIEW View_ProjetsTermines AS
SELECT
    P.titre,
    P.dateDebut,
    P.description,
    P.budget,
    P.nbEmploye,
    P.totalSalaire,
    C.nom AS client,
    P.statut
FROM
    Projets P
JOIN
    Clients C ON P.idClient = C.id
WHERE
    P.statut = 'Terminé';

--                                                                   Requêtes --                                                                  
/*
-- 1. Liste des employés avec leurs projets actuels
SELECT * FROM View_EmployesProjetsActuels;

-- 2. Liste des clients avec leur nombre de projets en cours
SELECT * FROM View_ListeClientsNbProjets;

-- 3. Liste des projets avec le nombre total d'heures travaillées
SELECT * FROM View_ProjetsNbHeuresTotal;

-- 4. Liste des employés avec leur salaire total
SELECT * FROM View_ListeEmployesSalaireTotal;

-- 5. Liste des projets terminés
SELECT * FROM View_ProjetsTermines;

-- 6.

-- 7.

-- 8. 

-- 9.

-- 10.
*/