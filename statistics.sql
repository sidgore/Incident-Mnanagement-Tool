-- Based on severity
SELECT `severity`,count(*) FROM bug_tracking_system.problem_table GROUP BY `severity`;

-- Based on Status
SELECT s.status_name, count(*) FROM problem_table as p INNER JOIN status_table as s ON p.status_id = s.status_id GROUP BY p.status_id;

-- Based on Assignee(Developer Name)
SELECT u.user_name, count(*) FROM problem_table as p INNER JOIN user_table as u ON p.assignee_user_id = u.user_id GROUP BY p.assignee_user_id;

-- Based on the Customer
SELECT c.customer_name, count(*) FROM problem_table as p INNER JOIN customer_table as c ON p.created_by = c.customer_id GROUP BY p.created_by;

-- Based on month
SELECT count(ticket_number), DATE_FORMAT(create_date, '%Y/%m') FROM problem_table GROUP BY MONTH(create_date);

-- Based on year
SELECT count(ticket_number), DATE_FORMAT(create_date, '%Y') FROM problem_table GROUP BY YEAR(create_date);

-- Get all developers on the basis of assignee_user_id
SELECT u.user_name, u.user_id FROM problem_table as p INNER JOIN user_table as u ON p.assignee_user_id = u.user_id GROUP BY p.assignee_user_id;
