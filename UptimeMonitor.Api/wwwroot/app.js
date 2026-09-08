const API_URL = '/api/sites';

async function fetchSites() {
    try {
        const response = await fetch(API_URL);
        const sites = await response.json();
        renderSites(sites);
    } catch (error) {
        console.error('Помилка при отриманні даних:', error);
        document.getElementById('sites-container').innerHTML = 'Помилка підключення до сервера.';
    }
}

function renderSites(sites) {
    const container = document.getElementById('sites-container');
    container.innerHTML = ''; 

    sites.forEach(site => {
        const card = document.createElement('div');
        
        let statusClass = 'pending';
        let statusText = 'Очікує перевірки';
        
        if (site.isOnline === true) {
            statusClass = 'online';
            statusText = 'Працює';
        } else if (site.isOnline === false) {
            statusClass = 'offline';
            statusText = 'Недоступний';
        }

        card.className = `card ${statusClass}`;
        
        // Форматуємо дату
        const lastChecked = site.lastChecked 
            ? new Date(site.lastChecked).toLocaleTimeString('uk-UA') 
            : '-';

        card.innerHTML = `
            <h3>${site.url}</h3>
            <p><strong>Статус:</strong> ${statusText}</p>
            <p><strong>Остання перевірка:</strong> ${lastChecked}</p>
        `;
        
        container.appendChild(card);
    });
}

fetchSites();

setInterval(fetchSites, 10000);