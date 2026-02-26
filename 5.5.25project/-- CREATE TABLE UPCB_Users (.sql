-- CREATE TABLE UPCB_Users (

--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Username NVARCHAR(50) UNIQUE NOT NULL, 
--     Password NVARCHAR(32) NOT NULL,
--     FullName NVARCHAR(50),
--     Email NVARCHAR(40),
--     balance   NOT NULL DEFAULT 0.00,
--     BirthYear INT NOT NULL,
--     gender NVARCHAR(40) NOT NULL,
--     Role NVARCHAR(10) DEFAULT 'user'
-- );

-- INSERT INTO UPCB_Users(Username, Password, FullName, Email,balance, BirthYear, gender, Role)
-- VALUES ('Ilay', 'Moapes', 'Moapes','mail@gmail.com',100000000.00,2009,'male', 'admin');

-- CREATE TABLE User_Skins (
--     id INT,
--     Skin1 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin2 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin3 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin4 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin5 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin6 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin7 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin8 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin9 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin10 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin11 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin12 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin13 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin14 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin15 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin16 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin17 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin18 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin19 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin20 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin21 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin22 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin23 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin24 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin25 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin26 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin27 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin28 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin29 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin30 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin31 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin32 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin33 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin34 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin35 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin36 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin37 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin38 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin39 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin40 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin41 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin42 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin43 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin44 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin45 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin46 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin47 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin48 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin49 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     Skin50 NVARCHAR(50) NOT NULL DEFAULT 'Empty',
--     FOREIGN KEY (id) REFERENCES UPCB_Users(Id)
-- )

-- DELETE User_Skins;

-- SELECT * FROM UPCB_Users;

-- DROP TABLE User_Skins;

-- INSERT INTO User_Skins (id, Skin1) VALUES
-- (15, '/Case_Skins/Fade_Skins/BF_Fade.png');


-- CREATE TABLE Skins (
--     Name NVARCHAR(100) NOT NULL,
--     ImagePath NVARCHAR(255) NOT NULL,
--     Price FLOAT NOT NULL,
-- );

-- SELECT * FROM User_Skins;



-- INSERT INTO Skins (Name, ImagePath, Price) VALUES
-- ('AWP Fade', '/Case_Skins/Fade_Skins/AWP_Fade.png', 861.23),
-- ('Butterfly Fade', '/Case_Skins/Fade_Skins/BF_Fade.png', 3895.45),
-- ('Huntsman Fade', '/Case_Skins/Fade_Skins/Hunts_M_Fade.png', 410.12),
-- ('R8 Revolver Fade', '/Case_Skins/Fade_Skins/R_Fade.png', 67.35),
-- ('Flip Knife Fade', '/Case_Skins/Fade_Skins/Flip_M_Fade.png', 478.46),
-- ('Glock-18 Fade', '/Case_Skins/Fade_Skins/GLOCK_Fade.png', 1674.45),
-- ('M4A1-S Fade', '/Case_Skins/Fade_Skins/M4S_Fade.png', 480.46),
-- ('M9 Bayonet Fade', '/Case_Skins/Fade_Skins/M9_Fade.png', 1793.35),
-- ('MAC-10 Amber Fade', '/Case_Skins/Fade_Skins/MAC_A_Fade.png', 16.35),
-- ('MAC-10 Fade', '/Case_Skins/Fade_Skins/MAC_Fade.png', 45.76),
-- ('Nomad Fade', '/Case_Skins/Fade_Skins/Nomad_Fade.png', 964.74),
-- ('P2000 Amber Fade', '/Case_Skins/Fade_Skins/P20_A_Fade.png', 4.68),
-- ('R8 Amber Fade', '/Case_Skins/Fade_Skins/R_A_Fade.png', 2.85),
-- ('USP-S Fade', '/Case_Skins/Fade_Skins/SD_Fade.png', 256.57),
-- ('Skeleton Knife Fade', '/Case_Skins/Fade_Skins/Skel_Fade.png', 1670.64),
-- ('Sawed-Off Amber Fade', '/Case_Skins/Fade_Skins/SO_A_Fade.png', 1.12),
-- ('SSG 08 Amber Fade', '/Case_Skins/Fade_Skins/SSG_A_Fade.png', 3.28),
-- ('UMP-45 Fade', '/Case_Skins/Fade_Skins/UMP_Fade.png', 76.45),
-- ('Ursus Fade', '/Case_Skins/Fade_Skins/Ursus_M_Fade.png', 356.23),
-- ('MP9 Fade', '/Case_Skins/Fade_Skins/MP9_Fade.png', 6.34);

-- ('AK-47 Asimov', '/Case_Skins/VIP_Skins/AK_ASMV.png', 295.24),
-- ('AK-47 Neon Rider', '/Case_Skins/VIP_Skins/AK_NR.png', 210.24),
-- ('AK-47 Predator', '/Case_Skins/VIP_Skins/AK_PD.png', 67.45),
-- ('AK-47 Redline', '/Case_Skins/VIP_Skins/AK_RDLN.png', 56.35),
-- ('AWP Electric Blue', '/Case_Skins/VIP_Skins/AWP_EB.png', 23.42),
-- ('AWP Fever', '/Case_Skins/VIP_Skins/AWP_FV.png', 17.53),
-- ('AWP Neo Noir', '/Case_Skins/VIP_Skins/AWP_NN.png', 45.90),
-- ('Desert Eagle Crimson', '/Case_Skins/VIP_Skins/D_CR.png', 95.47),
-- ('Five-SeveN Angry Mob', '/Case_Skins/VIP_Skins/FS_AM.png', 23.68),
-- ('Huntsman Safari Mesh', '/Case_Skins/VIP_Skins/HNTSMN_S.png', 170.35),
-- ('M4A4 Asiimov', '/Case_Skins/VIP_Skins/M4_ASMV.png', 430.35),
-- ('M4A4 Black Pearl', '/Case_Skins/VIP_Skins/M4_BK.png', 13.24),
-- ('M4A1-S Decimator', '/Case_Skins/VIP_Skins/M4_DS.png', 38.35),
-- ('M4A1-S Cyrex', '/Case_Skins/VIP_Skins/M4S_CF.png', 135.74),
-- ('M9 Bayonet Black Wax', '/Case_Skins/VIP_Skins/M9_BW.png', 756.49),
-- ('MAG-7 Turtle', '/Case_Skins/VIP_Skins/MG_TRTL.png', 247.46),
-- ('M4A4 Dark Water', '/Case_Skins/VIP_Skins/M4_DK.png', 64.24),
-- ('Navaja Fade', '/Case_Skins/VIP_Skins/NVJ_FADE.png', 183.00),
-- ('Navaja Marble Fade', '/Case_Skins/VIP_Skins/NVJ_MF.png', 479.35),
-- ('Talon BF', '/Case_Skins/VIP_Skins/TALON_BF.png', 420.23);


-- ('AK47 Bloodsport', '/Case_Skins/Mystery_Skins/AK_BLS.png', 265.23),
-- ('AWP Sand Mesh', '/Case_Skins/Mystery_Skins/AWP_SM.png', 0.23),
-- ('CZ75 Distress', '/Case_Skins/Mystery_Skins/CZ_DSTRS.png', 0.64),
-- ('CZ75 Polemic', '/Case_Skins/Mystery_Skins/CZ_PLMR.png', 1.32),
-- ('CZ75 Tigers', '/Case_Skins/Mystery_Skins/CZ_TGRS.png', 0.54),
-- ('Glock 18 Oxide Blaze', '/Case_Skins/Mystery_Skins/GLOCK_OB.png', 2.12),
-- ('Glock 18 Vogue', '/Case_Skins/Mystery_Skins/GLOCK_VG.png', 32.23),
-- ('M4A1-S Nightmare', '/Case_Skins/Mystery_Skins/M4S_NTMR.png', 72.32),
-- ('MAC-10 Disco Tech', '/Case_Skins/Mystery_Skins/MAC_DT.png', 18.46),
-- ('MAG-7 Heat', '/Case_Skins/Mystery_Skins/MAG_HEAT.png', 4.53),
-- ('Negev Tatter', '/Case_Skins/Mystery_Skins/NVJ_TT.png', 293.57),
-- ('P2000 Obsidian', '/Case_Skins/Mystery_Skins/P20_OBSDN.png', 9.36),
-- ('P2000 Urban Hazard', '/Case_Skins/Mystery_Skins/P20_UH.png', 2.57),
-- ('P90 Freight', '/Case_Skins/Mystery_Skins/P90_FRT.png', 13.54),
-- ('P250 Cassette', '/Case_Skins/Mystery_Skins/P250_CSTE.png', 0.32),
-- ('P250 Franklin', '/Case_Skins/Mystery_Skins/P250_FRNK.png', 0.87),
-- ('P250 Nevermore', '/Case_Skins/Mystery_Skins/P250_NVRMR.png', 1.04),
-- ('SG553 Orange Rail', '/Case_Skins/Mystery_Skins/SG_OR.png', 0.13),
-- ('Tec-9 Avalanche', '/Case_Skins/Mystery_Skins/TEC_AVLNC.png', 0.96);

-- ('AK47 Elite Build', '/Case_Skins/Hyper_Skins/AK_EB.png', 4.12),
-- ('AWP Hyper Beast', '/Case_Skins/Hyper_Skins/AWP_HB.png', 152.34),
-- ('Bowie Knife Slaughter', '/Case_Skins/Hyper_Skins/Bowie_Slter.png', 547.67),
-- ('Desert Eagle Bronze Deco', '/Case_Skins/Hyper_Skins/D_BD.png', 0.78),
-- ('Five Seven Hyper Beast', '/Case_Skins/Hyper_Skins/FS_HB.png', 34.82),
-- ('Five Seven Monkey Buisness', '/Case_Skins/Hyper_Skins/FS_MB.png', 43.53),
-- ('Five Seven Violent Daimyo', '/Case_Skins/Hyper_Skins/FS_VD.png', 0.82),
-- ('Glock 17 Wasteland Rebel', '/Case_Skins/Hyper_Skins/GLOCK_WR.png', 5.24),
-- ('M4A4-S Hyper Beast', '/Case_Skins/Hyper_Skins/M4S_HB.png', 132.65),
-- ('M4A4-S Leaded Glass', '/Case_Skins/Hyper_Skins/M4S_LG.png', 290.00),
-- ('M9 Bayonet Knife Lore', '/Case_Skins/Hyper_Skins/M9_Lore.png', 890.35),
-- ('MAC 10 Ocianic', '/Case_Skins/Hyper_Skins/MAC_OCNC.png', 50.00),
-- ('MP7 Armour Core', '/Case_Skins/Hyper_Skins/MP7_AC.png', 13.42),
-- ('Nova Hyper Beast', '/Case_Skins/Hyper_Skins/NOVA_HB.png', 43.70),
-- ('P250 Iron Clad', '/Case_Skins/Hyper_Skins/P250_IC.png', 2.32),
-- ('P250 Volcanic', '/Case_Skins/Hyper_Skins/P250_VLNC.png', 0.23),
-- ('PP Bizon High Roller', '/Case_Skins/Hyper_Skins/PP_HR.png', 12.42),
-- ('PP Bizon Harvester', '/Case_Skins/Hyper_Skins/PP_HRVSTR.png', 8.46),
-- ('SG Aerial', '/Case_Skins/Hyper_Skins/SG_Aerial.png', 0.12),
-- ('Sawed Off Origami', '/Case_Skins/Hyper_Skins/SO_ORGM.png', 0.23);



-- SELECT * FROM Skins
-- WHERE ImagePath LIKE '/Case_Skins/Fade_Skins/%';


-- INSERT INTO User_Skins (id) VALUES
-- (21);




SELECT * FROM UPCB_Users;

-- SELECT * FROM User_Skins ;


