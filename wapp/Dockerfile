FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5043

ENV ASPNETCORE_URLS=http://+:5043


RUN apt-get update
RUN apt-get update && \
    apt-get install  -y \
        apt-transport-https \
        arping \
        arptables \
        bridge-utils \
        ca-certificates \
        conntrack \
        curl \
        dnsutils \
        ethtool \
        gdb \
        iperf \
        iperf3 \
        iproute2 \
        nano \
        tar \
        ipset \
        iptables \
        iputils-ping \
        jq \
        kmod \
        ldap-utils \
        less \
        libpcap-dev \
        net-tools \
        netcat \
        netcat-openbsd \
        openssl \
        procps \
        nfs-kernel-server \
        psmisc \
       # python-dbg \
       # python3-dbg \
        socat \
        software-properties-common \
        strace \
        tcpdump \
        telnet \
        tmux \
       
        tree \
        vim \
        wget && \
    rm -rf /var/lib/apt/lists/*




# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
#RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
#USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["wapp/wapp.csproj", "wapp/"]
RUN dotnet restore "wapp/wapp.csproj"
COPY . .
WORKDIR "/src/wapp"
RUN dotnet build "wapp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "wapp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wapp.dll"]
