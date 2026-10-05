(() => {
    const header = document.querySelector('.p-header');
    const toggle = header?.querySelector('.p-menu');
    const nav = header?.querySelector('.p-nav');
    if (!header || !toggle || !nav) return;
    toggle.hidden = false;
    header.classList.add('is-enhanced');
    const close = () => {
        header.classList.remove('is-open');
        toggle.setAttribute('aria-expanded', 'false');
    };
    toggle.addEventListener('click', () => {
        const open = header.classList.toggle('is-open');
        toggle.setAttribute('aria-expanded', String(open));
    });
    header.addEventListener('keydown', event => {
        if (event.key === 'Escape' && header.classList.contains('is-open')) {
            close();
            toggle.focus();
        }
    });
    document.addEventListener('click', event => { if (!header.contains(event.target)) close(); });
    nav.addEventListener('click', event => { if (event.target.closest('a')) close(); });
    window.matchMedia('(min-width: 621px)').addEventListener('change', close);
    window.addEventListener('pageshow', close);
})();
