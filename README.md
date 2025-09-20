# 📘 Proyecto: Aplicación de Matrícula de Cursos Online

## 🎯 Objetivo
Desarrollar una plataforma web que permita a los usuarios inscribirse en cursos online, gestionar su progreso y a los administradores crear y administrar cursos.

## 🚀 Funcionalidades
- Catálogo de cursos (GET /courses)
- Detalle de curso (GET /courses/{id})
- Matrícula de cursos (POST /enrollments)
- Inscripciones de usuario (GET /enrollments/{userId})
- Creación de cursos (POST /courses) [Admin]
- Edición de cursos (PUT /courses/{id}) [Admin]
- Eliminación de cursos (DELETE /courses/{id}) [Admin]
- Autenticación JWT con roles (admin / alumno)

## 🛠️ Requisitos
- .NET 8 SDK
- SQL Server

## ▶️ Cómo ejecutar localmente
1. Clonar el repositorio  
   ```bash
   git clone https://github.com/UPT-FAING-EPIS/examen-2025-ii-si784-u1-dennisdhm7.git