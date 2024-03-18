cd "$(dirname "$0")"
docker-compose build
docker-compose up -d
read -p "Premi qualsiasi tasto per continuare . . ."