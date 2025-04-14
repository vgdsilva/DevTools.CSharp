import sys
import os

# Configurações
PASTA_TEMPLATE = "template"
PASTA_SAIDA = "ArquivosGerados"

# Verifica parâmetros
if len(sys.argv) < 2:
    print("Uso: python gerador.py Entidade1 Entidade2 ...")
    sys.exit(1)


entidades = sys.argv[1:]  # Pega os parâmetros

# Verifica/Cria pastas necessárias
os.makedirs(PASTA_SAIDA, exist_ok=True)

# Lê os templates
with open(f"{PASTA_TEMPLATE}/TemplateController.cs", "r") as f:
    controller_template = f.read()

with open(f"{PASTA_TEMPLATE}/TemplateDTO.cs", "r") as f:
    dto_template = f.read()

def gerar_arquivos(entidade):
    # Substitui placeholders (ajuste conforme seu template)
    controller = controller_template.replace("{{_Entity}}", entidade)
    dto = dto_template.replace("{{_Entity}}", entidade)
    
    # Salva os arquivos
    with open(f"{PASTA_SAIDA}/{entidade}Controller.cs", "w") as f:
        f.write(controller)
    
    with open(f"{PASTA_SAIDA}/{entidade}DTO.cs", "w") as f:
        f.write(dto)
    print(f"✅ Gerados: {entidade}Controller.cs e {entidade}DTO.cs")

for entidade in entidades:
    gerar_arquivos(entidade)