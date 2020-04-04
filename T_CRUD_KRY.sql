/*
 Navicat Premium Data Transfer

 Source Server         : ITINS_201
 Source Server Type    : Oracle
 Source Server Version : 110200
 Source Host           : 192.168.23.201:1521
 Source Schema         : ITINS

 Target Server Type    : Oracle
 Target Server Version : 110200
 File Encoding         : 65001

 Date: 04/04/2020 10:32:49
*/


-- ----------------------------
-- Table structure for T_CRUD_KRY
-- ----------------------------
DROP TABLE "ITINS"."T_CRUD_KRY";
CREATE TABLE "ITINS"."T_CRUD_KRY" (
  "NIK" NUMBER NOT NULL ,
  "NAMA" NVARCHAR2(255) ,
  "JK" NVARCHAR2(10) ,
  "ALAMAT" NVARCHAR2(500) ,
  "DEPARTEMEN" NVARCHAR2(255) ,
  "AKTIF" NUMBER 
)
TABLESPACE "ITINS"
LOGGING
NOCOMPRESS
PCTFREE 10
INITRANS 1
STORAGE (
  INITIAL 65536 
  NEXT 1048576 
  MINEXTENTS 1
  MAXEXTENTS 2147483645
  BUFFER_POOL DEFAULT
)
PARALLEL 1
NOCACHE
DISABLE ROW MOVEMENT
;

-- ----------------------------
-- Primary Key structure for table T_CRUD_KRY
-- ----------------------------
ALTER TABLE "ITINS"."T_CRUD_KRY" ADD CONSTRAINT "SYS_C0057953" PRIMARY KEY ("NIK");

-- ----------------------------
-- Checks structure for table T_CRUD_KRY
-- ----------------------------
ALTER TABLE "ITINS"."T_CRUD_KRY" ADD CONSTRAINT "SYS_C0057952" CHECK ("NIK" IS NOT NULL) NOT DEFERRABLE INITIALLY IMMEDIATE NORELY VALIDATE;
